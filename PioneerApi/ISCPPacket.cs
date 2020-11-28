namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class ISCPPacket {
		private const string HeaderMagic = "ISCP";

		private const byte ProtocolVersion = 0x01;
		private const int HeaderSize = 16;

		public ISCPMessage Message { get; }

		public ISCPPacket(ISCPMessage message) => this.Message = message;

		public int Size => ISCPPacket.HeaderSize + this.Message.Size;

		public static ISCPPacket Read(byte[] data, int initialOffset) {
			// ensure header matches
			int Offset = initialOffset;
			for (; Offset < initialOffset + ISCPPacket.HeaderMagic.Length; Offset++)
				if (data[Offset] != ISCPPacket.HeaderMagic[Offset - initialOffset])
					return null;

			// check the header size
			int HeaderSize = ISCPPacket.ReadInt(data, ref Offset);
			int DataSize = ISCPPacket.ReadInt(data, ref Offset);
			Offset += 4;

			// rest is all data bytes
			ISCPMessage Message = ISCPMessage.Read(data, Offset);
			if (Message == null) return null;

			return new ISCPPacket(Message);
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

		private static int ReadInt(byte[] buffer, ref int offset) {
			IEnumerable<byte> IntBytes = buffer.Skip(offset).Take(4);
			if (BitConverter.IsLittleEndian) IntBytes = IntBytes.Reverse();
			offset += 4;
			return BitConverter.ToInt32(IntBytes.ToArray(), 0);
		}
	}
}
