namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Diagnostics.Contracts;
	using System.IO;
	using System.Runtime.CompilerServices;
	using System.Text;

	public class ISCPMessage {
		private const string EndCharacter = "\u001A\r\n";
		private const string StartCharacter = "!";
		private const string DefaultDestinationCharacter = "1";

		public ISCPMessage(string command, string parameters, string destination = ISCPMessage.DefaultDestinationCharacter) {
			this.Command = command;
			this.Parameters = parameters;
			this.DestinationCharacter = destination;
		}

		public int Size => 5 + this.Command.Length + this.Parameters.Length;

		public string Command { get; }

		public string Parameters { get; }

		public string DestinationCharacter { get; }


		public static ISCPMessage Read(Stream dataStream) {
			if (dataStream.ReadByte() != Encoding.Default.GetBytes(ISCPMessage.StartCharacter)[0]) return null;

			dataStream.ReadByte(); // skip source byte

			List<byte> Buffer = new List<byte>();
			int Next;
			while ((Next = dataStream.ReadByte()) != 0x1A /* EOF */ && Next != -1) {
				Buffer.Add((byte)Next);
			}

			dataStream.ReadByte(); // \r
			dataStream.ReadByte(); // \n
			
			string Message = Encoding.Default.GetString(Buffer.ToArray());
			return new ISCPMessage(Message.Substring(0, 3), Message.Substring(3));
		}

		public byte[] Serialize() {
			string Composite = StartCharacter + DestinationCharacter + Command + Parameters + EndCharacter;
			return Encoding.Default.GetBytes(Composite);
		}
	}
}
