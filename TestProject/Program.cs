namespace TestProject {
	using System;
	using System.Drawing;
	using System.Linq;
	using System.Net.Sockets;
	using System.Runtime.CompilerServices;
	using System.Threading.Tasks;
	using PioneerApi;

	class Program {
		static async Task Main(string[] args) {
			Console.WriteLine("Hello World!");
			//byte[] Bytes = new ISCPPacket(new ISCPMessage("PWR", "01")).Serialize();

			TcpClient Client = new TcpClient("192.168.2.10", 60128);
			NetworkStream NetworkStream = Client.GetStream();

			Task ReadTask = Task.Run(() => ReadLoop(NetworkStream));

			string Command = "";
			while ((Command = Console.ReadLine()) is not "End" or null) {
				if (Command.Length < 3) continue;
				string CommandPart = Command.Substring(0, 3);
				string ParamsPart = Command.Substring(3);
				byte[] PacketBytes = new ISCPPacket(new ISCPMessage(CommandPart, ParamsPart)).Serialize();
				await NetworkStream.WriteAsync(PacketBytes);
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("Sent!");
				Console.ForegroundColor = ConsoleColor.White;
			}
			Console.WriteLine("End");
		}

		private static async void ReadLoop(NetworkStream stream) {
			byte[] Buffer = new byte[256];
			while (true) {
				int Read = await stream.ReadAsync(Buffer, 0, Buffer.Length);

				if (Read == 0) break;

				ISCPPacket Packet;
				int Offset = 0;
				while ((Packet = ISCPPacket.Read(Buffer, Offset)) == null) {
					Offset++;
				}

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Command: {0}, Params: {1}", Packet.Message.Command, Packet.Message.Parameters);
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
	}
}
