// -----------------------------------------------------------------------
// <copyright file="ApiClient.cs" company="John Lynch">
//   This file is licensed under the MIT license
//   Copyright (c) 2020 John Lynch
// </copyright>
// -----------------------------------------------------------------------

namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Net.Sockets;
	using System.Runtime.CompilerServices;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Xml.Serialization;

	/// <summary>
	///		API client for ISCP-compatible receivers
	/// </summary>
	public partial class ApiClient : IDisposable {
		/// <summary>
		///     The TCP client that communicates with the receiver
		/// </summary>
		private readonly TcpClient Client = new TcpClient();

		/// <summary>
		///		A resolver to use to combine separate jacket art packets
		/// </summary>
		private readonly JacketArtResolver JacketArtResolver;

		/// <summary>
		///     A dictionary of commands to their event handlers
		/// </summary>
		private readonly Dictionary<string, Action<ISCPPacket>> CommandEvents =
			new Dictionary<string, Action<ISCPPacket>>();

		/// <summary>
		///     The network stream to use to communicate with the receiver
		/// </summary>
		private Stream NetworkStream;

		/// <summary>
		///     Cancellation token for the read loop
		/// </summary>
		private CancellationTokenSource ReadCancellationToken;

		/// <summary>
		///     Initializes a new instance of the <see cref="ApiClient" /> class.
		/// </summary>
		public ApiClient() {
			this.RegisterAllCommands();
			this.JacketArtResolver = new JacketArtResolver(this);
			this.JacketArtResolver.OnJacketArtImage += (s,e) => this.OnJacketArtImage?.Invoke(this, e);
		}

		/// <summary>
		///		Event raised when a jacket art image is fully received
		/// </summary>
		public event EventHandler<JacketArtResolver.JacketArt> OnJacketArtImage;

		/// <summary>
		///     Connects to a receiver at the given IP address
		/// </summary>
		/// <param name="ip">The IP address to connect to</param>
		/// <param name="port">The port to connect to</param>
		/// <returns>When the client has connected</returns>
		public async Task ConnectAsync(string ip, int port = 60128) {
			await this.Client.ConnectAsync(ip, port);
			this.NetworkStream = this.Client.GetStream();
		}

		public Task SendCommandQuestion(string command) => this.SendCommandAsync(command, "QSTN");

		public Task SendCommandAsync(string command) => this.SendCommandAsync(command, "");

		public async Task SendCommandAsync(string command, string parameters) {
			if (this.NetworkStream == null)
				throw new InvalidOperationException(
					"Can't send message before connecting. Did you call APIClient.ConnectAsync()?");
			ISCPPacket Packet = new ISCPPacket(new ISCPMessage(command, parameters));
			byte[] WriteBytes = Packet.Serialize();

			await this.NetworkStream.WriteAsync(WriteBytes, 0, WriteBytes.Length);
		}

		/// <summary>
		///     Disposes resources used by this class
		/// </summary>
		public void Dispose() {
			this.Client?.Dispose();
			this.NetworkStream?.Dispose();
			this.ReadCancellationToken?.Dispose();
		}

		/// <summary>
		///     Starts listening for packets
		/// </summary>
		public void StartListening() {
			if (this.NetworkStream == null)
				throw new InvalidOperationException(
					"Can't start listening before connecting. Did you call APIClient.ConnectAsync()?");
			this.ReadCancellationToken = new CancellationTokenSource();
			Thread Thread = new Thread(this.ReadLoop) { IsBackground = true };
			Thread.Start();
		}

		/// <summary>
		///     Stops listening for packets
		/// </summary>
		public void StopListening() {
			this.ReadCancellationToken?.Cancel();
		}

		/// <summary>
		///		Disconnects from the receiver server
		/// </summary>
		public void Disconnect() {
			this.NetworkStream = null;
			this.Client.Close();
		}

		/// <summary>
		///     Callback for when a new packet is received.
		/// </summary>
		/// <param name="packet">The packet that was received</param>
		private void OnPacket(ISCPPacket packet) {
			string Command = packet.Message.Command;
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"{Command} {packet.Message.Parameters}");
			Console.ForegroundColor = ConsoleColor.White;
			if (this.CommandEvents.ContainsKey(Command)) this.CommandEvents[Command](packet);
		}

		/// <summary>
		///     Starts the packet read loop from the network stream
		/// </summary>
		private async void ReadLoop() {
			try {
				while (!this.ReadCancellationToken.Token.IsCancellationRequested && this.Client.Connected) {
					ISCPPacket Next = await ISCPPacket.Read(this.NetworkStream);
					this.OnPacket(Next);
				}
			}
			catch (OperationCanceledException) {
				// ignored
			} catch (IOException) {
				// ignored
			}
		}

		/// <summary>
		///     Registers all suitable commands for event handlers
		/// </summary>
		// ReSharper disable once FunctionComplexityOverflow "Method is too complex to analyze" haha but it's not like it's confusing.
		private void RegisterAllCommands() {
			this.RegisterCommand(CommandId.MasterPower, () => this.OnMasterPower, s => s == "01");
			this.RegisterCommand(CommandId.AudioMuting, () => this.OnAudioMuting);
			this.RegisterCommand(CommandId.SpeakerAControl, () => this.OnSpeakerAControl);
			this.RegisterCommand(CommandId.SpeakerBControl, () => this.OnSpeakerBControl);
			this.RegisterCommand(CommandId.SpeakerLayout, () => this.OnSpeakerLayout);
			this.RegisterCommand(CommandId.MasterVolume, () => this.OnMasterVolume, s => Int32.Parse(s, NumberStyles.HexNumber));
			this.RegisterCommand(CommandId.ToneFront, () => this.OnToneFront);
			this.RegisterCommand(CommandId.ToneFrontWide, () => this.OnToneFrontWide);
			this.RegisterCommand(CommandId.ToneFrontHigh, () => this.OnToneFrontHigh);
			this.RegisterCommand(CommandId.ToneCenter, () => this.OnToneCenter);
			this.RegisterCommand(CommandId.ToneSurround, () => this.OnToneSurround);
			this.RegisterCommand(CommandId.ToneSurroundBack, () => this.OnToneSurroundBack);
			this.RegisterCommand(CommandId.ToneSubwoofer, () => this.OnToneSubwoofer);
			this.RegisterCommand(CommandId.PhaseMatchingBassControl, () => this.OnPhaseMatchingBassControl);
			this.RegisterCommand(CommandId.SleepControl, () => this.OnSleepControl);
			this.RegisterCommand(CommandId.SpeakerLevelCalibration, () => this.OnSpeakerLevelCalibration);
			this.RegisterCommand(CommandId.SubwooferLevelControl, () => this.OnSubwooferLevelControl);
			this.RegisterCommand(CommandId.Subwoofer2LevelControl, () => this.OnSubwoofer2LevelControl);
			this.RegisterCommand(CommandId.CenterLevelControl, () => this.OnCenterLevelControl);
			this.RegisterCommand(CommandId.DisplayInformationControl, () => this.OnDisplayInformationControl);
			this.RegisterCommand(CommandId.DimmerControl, () => this.OnDimmerControl);
			this.RegisterCommand(CommandId.SetupOperationControl, () => this.OnSetupOperationControl);
			this.RegisterCommand(CommandId.MemorySetupControl, () => this.OnMemorySetupControl);
			this.RegisterCommand(CommandId.AudioInformation, () => this.OnAudioInformation);
			this.RegisterCommand(CommandId.VideoInformation, () => this.OnVideoInformation);
			this.RegisterCommand(CommandId.InputSelection, () => this.OnInputSelection);
			this.RegisterCommand(CommandId.AudioSelection, () => this.OnAudioSelection);
			this.RegisterCommand(CommandId.TriggerAControl, () => this.OnTriggerAControl);
			this.RegisterCommand(CommandId.TriggerBControl, () => this.OnTriggerBControl);
			this.RegisterCommand(CommandId.TriggerCControl, () => this.OnTriggerCControl);
			this.RegisterCommand(CommandId.HDMIOutSelection, () => this.OnHDMIOutSelection);
			this.RegisterCommand(CommandId.HDMIAudioOutSelectionMain, () => this.OnHDMIAudioOutSelectionMain);
			this.RegisterCommand(CommandId.HDMIAudioOutSelectionSub, () => this.OnHDMIAudioOutSelectionSub);
			this.RegisterCommand(CommandId.HDMICECEnabled, () => this.OnHDMICECEnabled);
			this.RegisterCommand(CommandId.HDMICECControlMonitorType, () => this.OnHDMICECControlMonitorType);
			this.RegisterCommand(CommandId.OutputResolution, () => this.OnOutputResolution);
			this.RegisterCommand(CommandId.VideoWideMode, () => this.OnVideoWideMode);
			this.RegisterCommand(CommandId.VideoPictureMode, () => this.OnVideoPictureMode);
			this.RegisterCommand(CommandId.ListeningMode, () => this.OnListeningMode);
			this.RegisterCommand(CommandId.LateNightSetting, () => this.OnLateNightSetting);
			this.RegisterCommand(CommandId.ReEQCinemaFilterEnabled, () => this.OnReEQCinemaFilterEnabled);
			this.RegisterCommand(CommandId.AudysseyMultEQEnabled, () => this.OnAudysseyMultEQEnabled);
			this.RegisterCommand(CommandId.AudysseyDynamicEQEnabled, () => this.OnAudysseyDynamicEQEnabled);
			this.RegisterCommand(CommandId.AudysseyDynamicVolumeEnabled, () => this.OnAudysseyDynamicVolumeEnabled);
			this.RegisterCommand(CommandId.DolbyVolumeLevel, () => this.OnDolbyVolumeLevel);
			this.RegisterCommand(CommandId.AccuEQEnabled, () => this.OnAccuEQEnabled);
			this.RegisterCommand(CommandId.MusicOptimizerEnabled, () => this.OnMusicOptimizerEnabled);
			this.RegisterCommand(CommandId.AVSyncConfiguration, () => this.OnAVSyncConfiguration);
			this.RegisterCommand(CommandId.SmartGridEcoConfiguration, () => this.OnSmartGridEcoConfiguration);
			this.RegisterCommand(CommandId.Update, () => this.OnUpdate);
			this.RegisterCommand(CommandId.PopupMessage, () => this.OnPopupMessage);
			this.RegisterCommand(CommandId.Tuning, () => this.OnTuning);
			this.RegisterCommand(CommandId.TuningPresent, () => this.OnTuningPresent);
			this.RegisterCommand(CommandId.TuningPresetMemoryControl, () => this.OnTuningPresetMemoryControl);
			this.RegisterCommand(CommandId.RDSInformation, () => this.OnRDSInformation);
			this.RegisterCommand(CommandId.PTYScan, () => this.OnPTYScan);
			this.RegisterCommand(CommandId.TPScan, () => this.OnTPScan);
			this.RegisterCommand(CommandId.HDRadioArtistName, () => this.OnHDRadioArtistName);
			this.RegisterCommand(CommandId.HDRadioChannelName, () => this.OnHDRadioChannelName);
			this.RegisterCommand(CommandId.HDRadioTitle, () => this.OnHDRadioTitle);
			this.RegisterCommand(CommandId.HDRadioDetail, () => this.OnHDRadioDetail);
			this.RegisterCommand(CommandId.HDRadioChannelProgram, () => this.OnHDRadioChannelProgram);
			this.RegisterCommand(CommandId.HDRadioBlendMode, () => this.OnHDRadioBlendMode);
			this.RegisterCommand(CommandId.HDRadioTunerState, () => this.OnHDRadioTunerState);
			this.RegisterCommand(CommandId.NetworkRemoteControl, () => this.OnNetworkRemoteControl);
			this.RegisterCommand(CommandId.BluetoothControl, () => this.OnBluetoothControl);
			this.RegisterCommand(CommandId.NetworkArtistName, () => this.OnNetworkArtistName);
			this.RegisterCommand(CommandId.NetworkAlbumName, () => this.OnNetworkAlbumName);
			this.RegisterCommand(CommandId.NetworkTitle, () => this.OnNetworkTitle);
			this.RegisterCommand(CommandId.NetworkTimeInfo, () => this.OnNetworkTimeInfo, ApiClient.DeserializeDurations);
			this.RegisterCommand(CommandId.NetworkTrackInfo, () => this.OnNetworkTrackInfo);
			this.RegisterCommand(CommandId.NetworkPlayStatus, () => this.OnNetworkPlayStatus, ApiClient.PlayInfo.Parse);
			this.RegisterCommand(CommandId.NetworkMenuStatus, () => this.OnNetworkMenuStatus);
			this.RegisterCommand(CommandId.NetworkTimeSeek, () => this.OnNetworkTimeSeek);
			this.RegisterCommand(CommandId.InternetRadioPreset, () => this.OnInternetRadioPreset);
			this.RegisterCommand(CommandId.NetworkConnectionStatus, () => this.OnNetworkConnectionStatus);
			this.RegisterCommand(CommandId.NetworkListInfo, () => this.OnNetworkListInfo);
			this.RegisterCommand(CommandId.NetworkListInfoAll, () => this.OnNetworkListInfoAll, ApiClient.ListItemsDeserializer);
			this.RegisterCommand(CommandId.NetworkJacketArt, () => this.OnNetworkJacketArt);
			this.RegisterCommand(CommandId.NetworkServiceSelection, () => this.OnNetworkServiceSelection);
			this.RegisterCommand(CommandId.NetworkKeyboard, () => this.OnNetworkKeyboard);
			this.RegisterCommand(CommandId.NetworkPopup, () => this.OnNetworkPopup);
			this.RegisterCommand(CommandId.NetworkListTitleInfo, () => this.OnNetworkListTitleInfo, ApiClient.NetworkListTitleInfo.Parse);
			this.RegisterCommand(CommandId.IPodModeChange, () => this.OnIPodModeChange);
			this.RegisterCommand(CommandId.NetworkStandby, () => this.OnNetworkStandby);
			this.RegisterCommand(CommandId.ReceiverNetworkInformation, () => this.OnReceiverNetworkInformation, ApiClient.ReceiverInformationDeserializer);
			this.RegisterCommand(CommandId.NetworkCustomPopupMessage, () => this.OnNetworkCustomPopupMessage);
			this.RegisterCommand(CommandId.NetworkListUpdate, () => this.OnNetworkListUpdate);
			this.RegisterCommand(CommandId.NetworkPlaybackStatus, () => this.OnNetworkPlaybackStatus);
			this.RegisterCommand(CommandId.AirplayArtistName, () => this.OnAirplayArtistName);
			this.RegisterCommand(CommandId.AirplayAlbumName, () => this.OnAirplayAlbumName);
			this.RegisterCommand(CommandId.AirplayTitleName, () => this.OnAirplayTitleName);
			this.RegisterCommand(CommandId.AirplayTimeInfo, () => this.OnAirplayTimeInfo);
			this.RegisterCommand(CommandId.AirplayPlayStatus, () => this.OnAirplayPlayStatus);
		}

		private static ReceiverInformationResponse ReceiverInformationDeserializer(string xml) {
			XmlSerializer Serializer = new XmlSerializer(typeof(ReceiverInformationResponse));
			using (StringReader Reader = new StringReader(xml)) {
				return (ReceiverInformationResponse)Serializer.Deserialize(Reader);
			}
		}

		private static ListItemsResponse ListItemsDeserializer(string xml) {
			XmlSerializer Serializer = new XmlSerializer(typeof(ListItemsResponse));
			using (StringReader Reader = new StringReader(xml.Substring(9))) {
				return (ListItemsResponse)Serializer.Deserialize(Reader);
			}
		}

		/// <summary>
		///		Deserializes durations of the format "mm:ss/mm:ss" or "hh:mm:ss/hh:mm:ss"
		/// </summary>
		/// <param name="time">The string to deserialize</param>
		/// <returns>The deserialized string in the format (Part, Total)</returns>
		private static (TimeSpan?, TimeSpan?) DeserializeDurations(string time) {
			TimeSpan? DeserializePart(string part) {
				string[] PartParts = part.Split(':');
				if (PartParts.Any(p => p == "--")) return null;
				int Seconds = Int32.Parse(PartParts[PartParts.Length - 1]);
				int Minutes = Int32.Parse(PartParts[PartParts.Length - 2]);
				int Hours = PartParts.Length == 3 ? Int32.Parse(PartParts[0]) : 0;
				return new TimeSpan(Hours, Minutes, Seconds);
			}

			string[] TimeParts = time.Split('/');

			return (DeserializePart(TimeParts[0]), DeserializePart(TimeParts[1]));
		}
		
		/// <summary>
		///     Registers an event handler for a command
		/// </summary>
		/// <typeparam name="T">The type of data the command provides</typeparam>
		/// <param name="command">The command to register an event for</param>
		/// <param name="eventHandler">A method that will get the current value of the event handler</param>
		/// <param name="deserializer">A function that will convert the parameters into type <typeparamref name="T" /></param>
		private void RegisterCommand<T>(
			string command,
			Func<EventHandler<T>> eventHandler,
			Func<string, T> deserializer) {
			this.CommandEvents.Add(
				command,
				packet => eventHandler()?.Invoke(this, deserializer(packet.Message.Parameters)));
		}

		/// <summary>
		///     Registers a string event handler for a command
		/// </summary>
		/// <param name="command">The command to register an event for</param>
		/// <param name="eventHandler">A method that will get the current value of the event handler</param>
		private void RegisterCommand(string command, Func<EventHandler<string>> eventHandler) =>
			this.RegisterCommand(command, eventHandler, s => s);
	}
}