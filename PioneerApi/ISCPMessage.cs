namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Diagnostics.Contracts;
	using System.Runtime.CompilerServices;
	using System.Text;

	public class ISCPMessage {
		private const string EndCharacter = "\u001A\r\n";
		private const string StartCharacter = "!";
		private const string DestinationCharacter = "1";

		public ISCPMessage(string command, string parameters) {
			this.Command = command;
			this.Parameters = parameters;
		}

		public int Size => 5 + this.Command.Length + this.Parameters.Length;

		public string Command { get; }

		public string Parameters { get; }

		public static ISCPMessage Read(byte[] data, int offset) {
			if (data[offset++] != Encoding.Default.GetBytes(ISCPMessage.StartCharacter)[0]) return null;
			offset++; // source character
			int Start = offset;
			while (data[offset] != 0x1A) {
				offset++;
			}

			string Message = Encoding.Default.GetString(data, Start, offset - Start);
			return new ISCPMessage(Message.Substring(0, 3), Message.Substring(3));
		}

		public byte[] Serialize() {
			string Composite = StartCharacter + DestinationCharacter + Command + Parameters + EndCharacter;
			return Encoding.Default.GetBytes(Composite);
		}
	}
}
