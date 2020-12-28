// -----------------------------------------------------------------------
// <copyright file="DiscoveryService.cs" company="John Lynch">
//   This file is licensed under the MIT license.
//   Copyright (c) 2018 John Lynch
// </copyright>
// -----------------------------------------------------------------------

namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Net.Sockets;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;

	/// <summary>
	///     Discovers ISCP clients on the local network
	/// </summary>
	public class DiscoveryService {
		/// <summary>
		///     The port to broadcast the probe on
		/// </summary>
		private const int BroadcastPort = 60128;

		/// <summary>
		///     The source of the token used to cancel broadcast packets
		/// </summary>
		private CancellationTokenSource BroadcastCancellationTokenSource;

		/// <summary>
		///     A list of discovered MAC addresses
		/// </summary>
		private HashSet<string> DiscoveredMacs = new HashSet<string>();

		/// <summary>
		///     Initializes a new instance of the <see cref="DiscoveryService" /> class.
		/// </summary>
		public DiscoveryService() {
		}

		/// <summary>
		///     Event called when a hub was found
		/// </summary>
		public event EventHandler<DiscoveredDevice> DeviceFound;

		/// <summary>
		///     Gets or sets the delay between discovery probes broadcasted over the network
		/// </summary>
		public int RebroadcastDelay { get; set; } = 5000;

		/// <summary>
		///     Broadcasts availability and waits for clients to identify
		/// </summary>
		public void StartDiscovery() {
			this.BroadcastCancellationTokenSource = new CancellationTokenSource();
			this.BroadcastContinually(this.BroadcastCancellationTokenSource.Token);
		}

		/// <summary>
		///     Stops listening for clients
		/// </summary>
		public void StopDiscovery() {
			this.BroadcastCancellationTokenSource.Cancel();
		}

		/// <summary>
		///     Gets the device's local broadcast addresses
		/// </summary>
		/// <returns>A list of all interfaces' IPv4 broadcast addresses</returns>
		private static IEnumerable<string> GetLocalBroadcastAddresses() {
			IPHostEntry Host = Dns.GetHostEntry(Dns.GetHostName());
			return Host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).Select(
				t => {
					byte[] IPBytes = t.GetAddressBytes();
					IPBytes[3] = 255;
					return new IPAddress(IPBytes).ToString();
				});
		}

		/// <summary>
		///     Broadcasts the probe to local broadcast addresses, falling back to 255.255.255.255
		/// </summary>
		private void Broadcast() {
			try {
				foreach (string Address in DiscoveryService.GetLocalBroadcastAddresses())
					this.BroadcastTo(Address);
			} catch (SocketException) {
				this.BroadcastTo("255.255.255.255");
			}
		}

		/// <summary>
		///     Continually broadcasts the probe every 3 seconds
		/// </summary>
		/// <param name="token">A token used to cancel the broadcasts</param>
		private async void BroadcastContinually(CancellationToken token) {
			try {
				while (!token.IsCancellationRequested) {
					this.Broadcast();
					await Task.Delay(this.RebroadcastDelay, token);
				}
			} catch (OperationCanceledException) {
				// token has been canceled. exit
			}
		}

		/// <summary>
		///     Broadcasts the probe to a specific IP address
		/// </summary>
		/// <param name="ipAddress">The IPv4 address to broadcast to</param>
		private async void BroadcastTo(string ipAddress) {
			Socket UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) {
				                      EnableBroadcast = true,
				                      MulticastLoopback = false
			                      };

			IPAddress IPAddress = IPAddress.Parse(ipAddress);
			UDPSocket.Bind(new IPEndPoint(IPAddress.Any, 0));

			// send
			IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress, DiscoveryService.BroadcastPort);
			byte[] ProbeBytes = this.GetProbe();
			UDPSocket.SendTo(ProbeBytes, RemoteEndPoint);
			
			// and listen
			try {
				CancellationTokenSource ListenToken = new CancellationTokenSource(TimeSpan.FromSeconds(5));
				UDPSocket.ReceiveTimeout = 5000;
				EndPoint From = new IPEndPoint(0, 0);
				while (!ListenToken.IsCancellationRequested) {
					byte[] Buffer = new byte[255];

					int Read = await Task.Factory.FromAsync(
						           UDPSocket.BeginReceiveFrom(
							           Buffer,
							           0,
							           Buffer.Length,
							           SocketFlags.None,
							           ref From,
							           null,
							           null),
						           (r) => UDPSocket.EndReceiveFrom(r, ref From));
					if (Read <= 0) break;

					MemoryStream Stream = new MemoryStream(Buffer, 0, Read);
					Stream.Seek(0, SeekOrigin.Begin);
					ISCPPacket Packet = await ISCPPacket.Read(Stream);
					DiscoveredDevice Device = DiscoveredDevice.Parse(Packet.Message, From);
					if (this.DiscoveredMacs.Contains(Device.MacAddress)) return;
					this.DiscoveredMacs.Add(Device.MacAddress);

					this.DeviceFound?.Invoke(this, Device);
				}
			} finally {
				UDPSocket.Close();
			}
		}

		/// <summary>
		///     Gets the probe that should be broadcast
		/// </summary>
		/// <returns>The newline separated probe</returns>
		private byte[] GetProbe() =>
			new ISCPPacket(new ISCPMessage("ECN", "QSTN", "p")).Serialize();
	}
}