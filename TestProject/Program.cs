// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="John Lynch">
//   This file is licensed under the MIT license
//   Copyright (c) 2020 John Lynch
// </copyright>
// -----------------------------------------------------------------------

namespace TestProject {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Sockets;
	using System.Runtime.CompilerServices;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Threading.Tasks;
	using System.Xml.Serialization;

	using PioneerApi;

	internal class Program {
		private static string FlagNext = string.Empty;

		private static async Task DemoOne() {
			TcpClient Client = new TcpClient("192.168.1.120", 60128);
			Stream NetworkStream = Client.GetStream();

			Task ReadTask = Task.Run(() => Program.ReadLoop(NetworkStream));

			string Command = string.Empty;
			while ((Command = Console.ReadLine()) is not "End" or null) {
				if (Command.Length < 3) continue;
				string CommandPart = Command.Substring(0, 3);
				string ParamsPart = Command.Substring(3);
				byte[] PacketBytes = new ISCPPacket(new ISCPMessage(CommandPart, ParamsPart)).Serialize();

				/*for (int i = 0; i < PacketBytes.Length; i += 4) {
					byte?[] DisplayBytes = PacketBytes.Skip(i).Take(4).Cast<byte?>().ToArray();
					while (DisplayBytes.Length < 4) {
						DisplayBytes = DisplayBytes.Concat(new byte?[] { null }).ToArray();
					}
					string Output = String.Join(
						" ",
						DisplayBytes.Select(x => x == null ? "  " : x.Value.ToString("X2")));
					Output += "\t";
					Output += String.Join(" ", DisplayBytes.Select(t => t == null ? " " : (t > 32 && t < 126 ? ((char)t).ToString() : ".")));
					Console.WriteLine(Output);
				}*/
				await NetworkStream.WriteAsync(PacketBytes);
				Program.FlagNext = CommandPart;
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("Sent!");
				Console.ForegroundColor = ConsoleColor.White;
			}

			Console.WriteLine("End");
		}

		private static async Task DemoTwo() {
			ApiClient Client = new ApiClient();
			await Client.ConnectAsync("192.168.1.120");
			Console.WriteLine("Connected!");
			await Client.SendCommandQuestion(CommandId.NetworkTitle);
			await Client.SendCommandQuestion(CommandId.NetworkAlbumName);
			await Client.SendCommandQuestion(CommandId.NetworkArtistName);
			await Client.SendCommandQuestion(CommandId.NetworkPlayStatus);
			await Client.SendCommandAsync(CommandId.NetworkJacketArt, "REQ");
			Console.WriteLine("And done!");

			Client.OnNetworkTitle += (s, title) => Console.WriteLine("Title: \"{0}\"", title);
			Client.OnNetworkTimeInfo += (s, time) => Console.WriteLine("Elapsed: {0}, Total: {1}", time.Item1, time.Item2);
			Client.OnJacketArtImage += (s, image) => {
				Console.WriteLine("IMAGE!");

				File.WriteAllBytes(@"C:\Users\jlync\Downloads\outpic.bmp", image.Data);
				Console.WriteLine("IMAGE!");
			};
			Client.StartListening();
		}

		private static async Task DemoThree() {
			DiscoveryService Service = new DiscoveryService();
			Service.DeviceFound += (s, d) => {
				//Console.WriteLine("Command: {0}, Params: {1}", d.Command, d.Parameters);
			};
			Service.StartDiscovery();
			await Task.Delay(10000);
			Service.StopDiscovery();
			Console.WriteLine("stopped");
			await Task.Delay(10000);
		}


		private static async Task DemoFour() {
			ApiClient Client = new ApiClient();
			await Client.ConnectAsync("192.168.1.120");
			Console.WriteLine("Connected!");
			Client.OnReceiverNetworkInformation += (s, info) => {
				Console.WriteLine($"{info.Device.Brand} {info.Device.Category} {info.Device.Model}");
			};
			Client.StartListening();
			await Client.SendCommandQuestion(CommandId.ReceiverNetworkInformation);
			await Task.Delay(15000);
		}

		private static async Task Main(string[] args) {
			Console.WriteLine("Ready!");
			await Program.DemoThree();
		}

		private static async void ReadLoop(Stream stream) {
			ISCPPacket Packet;

			while ((Packet = await ISCPPacket.Read(stream)) != null) {
				if (Program.FlagNext == Packet.Message.Command) {
					Program.FlagNext = string.Empty;
					Console.ForegroundColor = ConsoleColor.Green;
				}
				else {
					Console.ForegroundColor = ConsoleColor.DarkGray;
				}

				string CommandName = CommandId.Commands.ContainsKey(Packet.Message.Command)
					                     ? CommandId.Commands[Packet.Message.Command].FriendlyName
					                     : Packet.Message.Command;
				Console.WriteLine("Command: {0}, Params: {1}", CommandName, Packet.Message.Parameters);
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
	}
}

