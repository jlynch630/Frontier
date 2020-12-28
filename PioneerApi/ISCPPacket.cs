namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ISCPPacket {
		private const string HeaderMagic = "ISCP";

		private const byte ProtocolVersion = 0x01;
		private const int HeaderSize = 16;

		public ISCPMessage Message { get; }

		public ISCPPacket(ISCPMessage message) => this.Message = message;

		public int Size => ISCPPacket.HeaderSize + this.Message.Size;

		public static async Task<ISCPPacket> Read(Stream dataStream) {
			// ensure header matches
			while (dataStream.ReadByte() != ISCPPacket.HeaderMagic[0]) {
			}

			for (int i = 1; i < 4; i++) {
				// if the header doesn't end up matching all the way, read again
				if (dataStream.ReadByte() != ISCPPacket.HeaderMagic[i])
					return await ISCPPacket.Read(dataStream);
			}

			// check the header size
			int HeaderSize = await ISCPPacket.ReadInt(dataStream);
			int DataSize = await ISCPPacket.ReadInt(dataStream);
			byte Version = (byte)dataStream.ReadByte();

			// skip reserved bytes
			for (int i = 0; i < 3; i++)
				dataStream.ReadByte();

			// rest is all data bytes
			ISCPMessage Message = ISCPMessage.Read(dataStream);

			// if it failed, try again
			return Message == null ? await ISCPPacket.Read(dataStream) : new ISCPPacket(Message);
		}

		public byte[] Serialize() {
			byte[] Buffer = new byte[this.Size];

			// I S C P
			byte[] HeaderBytes = Encoding.Default.GetBytes(ISCPPacket.HeaderMagic);
			Array.Copy(HeaderBytes, Buffer, HeaderBytes.Length);
			int Offset = 4;

			// Header Size, Data Size, Version and Reserved
			Offset = ISCPPacket.AddInt(Buffer, Offset, ISCPPacket.HeaderSize);
			Offset = ISCPPacket.AddInt(Buffer, Offset, this.Message.Size);
			Buffer[Offset++] = ISCPPacket.ProtocolVersion;

			// skip reserved bytes
			Offset += 3;

			byte[] DataBytes = this.Message.Serialize();
			Array.Copy(DataBytes, 0, Buffer, Offset, DataBytes.Length);
			return Buffer;
		}

		private static byte[] GetBigEndianInt(int i) {
			byte[] Bytes = BitConverter.GetBytes(i);
			return BitConverter.IsLittleEndian ? Bytes.Reverse().ToArray() : Bytes;
		}

		private static int AddInt(byte[] buffer, int offset, int i) {
			byte[] IntBytes = ISCPPacket.GetBigEndianInt(i);
			Array.Copy(IntBytes, 0, buffer, offset, IntBytes.Length);
			return offset + IntBytes.Length;
		}

		private static async Task<int> ReadInt(Stream dataStream) {
			byte[] IntBytes = new byte[4];
			int Read = await dataStream.ReadAsync(IntBytes, 0, 4);
			if (Read != 4) return -1;

			if (BitConverter.IsLittleEndian) IntBytes = IntBytes.Reverse().ToArray();
			return BitConverter.ToInt32(IntBytes, 0);
		}
	}
}
