// -----------------------------------------------------------------------
// <copyright file="ConnectionService.cs" company="John Lynch">
//   This file is licensed under the MIT license
//   Copyright (c) 2020 John Lynch
// </copyright>
// -----------------------------------------------------------------------

namespace Frontier {
	using System;
	using System.Threading.Tasks;

	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Util;
	using PioneerApi;

	[Service(Name = "com.jlync.frontier.ConnectionService")]
	public class ConnectionService : Service {
		public event EventHandler<PlaybackState> OnPlaybackStateChanged;

		public JacketArtResolver.JacketArt Art { get; private set; }

		public IBinder Binder { get; private set; }

		public ApiClient Client { get; private set; }

		public PlaybackState CurrentState { get; } = new PlaybackState();

		public bool IsPoweredOn { get; private set; }

		public int MasterVolume { get; private set; }

		public ReceiverInformationResponse ReceiverInfo { get; private set; }

		public override IBinder? OnBind(Intent? intent) {
			this.Binder = new ConnectionBinder(this);
			return this.Binder;
		}

		public override async void OnCreate() {
			base.OnCreate();

			this.Client = new ApiClient();
			this.RegisterEvents();
		}

		public override void OnDestroy() {
			this.Client.StopListening();
			this.Client.Disconnect();
			this.UnregisterEvents();
			this.Client = null;

			base.OnDestroy();
		}

		public async Task Connect(string ip) {
			int Port = 60128;
			if (ip.Contains(":")) {
				string[] Parts = ip.Split(":");
				ip = Parts[0];
				Port = Int32.Parse(Parts[1]);
			}

			await this.Client.ConnectAsync(ip, Port);
			this.Client.StartListening();
			await this.RequestState();
		}

		private void OnJacketArtImage(object sender, JacketArtResolver.JacketArt art) {
			this.Art = art;
		}

		private void OnMasterPower(object sender, bool isOn) {
			this.IsPoweredOn = isOn;
		}

		private void OnMasterVolume(object sender, int volume) {
			this.MasterVolume = volume;
		}

		private void OnNetworkAlbum(object sender, string album) {
			this.CurrentState.Album = album;
			this.OnPlaybackStateChanged?.Invoke(this, this.CurrentState);
		}

		private void OnNetworkArtist(object sender, string artist) {
			this.CurrentState.Artist = artist;
			this.OnPlaybackStateChanged?.Invoke(this, this.CurrentState);
		}

		private void OnNetworkPlayState(object sender, ApiClient.PlayInfo playInfo) {
			this.CurrentState.PlayStatus = playInfo;
			this.OnPlaybackStateChanged?.Invoke(this, this.CurrentState);
		}

		private void OnNetworkTitle(object sender, string title) {
			this.CurrentState.Title = title;
			this.OnPlaybackStateChanged?.Invoke(this, this.CurrentState);
		}

		private void OnReceiverNetworkInformation(object sender, ReceiverInformationResponse info) =>
			this.ReceiverInfo = info;

		private void RegisterEvents() {
			this.Client.OnNetworkTitle += this.OnNetworkTitle;
			this.Client.OnNetworkArtistName += this.OnNetworkArtist;
			this.Client.OnNetworkAlbumName += this.OnNetworkAlbum;
			this.Client.OnNetworkPlayStatus += this.OnNetworkPlayState;
			this.Client.OnMasterVolume += this.OnMasterVolume;
			this.Client.OnMasterPower += this.OnMasterPower;
			this.Client.OnJacketArtImage += this.OnJacketArtImage;
			this.Client.OnReceiverNetworkInformation += this.OnReceiverNetworkInformation;
		}

		private async Task RequestState() {
			// playback info
			await this.Client.SendCommandQuestion(CommandId.NetworkTitle);
			await this.Client.SendCommandQuestion(CommandId.NetworkAlbumName);
			await this.Client.SendCommandQuestion(CommandId.NetworkArtistName);
			await this.Client.SendCommandQuestion(CommandId.NetworkPlayStatus);
			await this.Client.SendCommandAsync(CommandId.NetworkJacketArt, "REQ");

			// volume
			await this.Client.SendCommandQuestion(CommandId.MasterVolume);

			// power
			await this.Client.SendCommandQuestion(CommandId.MasterPower);

			// info
			await this.Client.SendCommandQuestion(CommandId.ReceiverNetworkInformation);
		}

		private void UnregisterEvents() {
			this.Client.OnNetworkTitle -= this.OnNetworkTitle;
			this.Client.OnNetworkArtistName -= this.OnNetworkArtist;
			this.Client.OnNetworkAlbumName -= this.OnNetworkAlbum;
			this.Client.OnNetworkPlayStatus -= this.OnNetworkPlayState;
			this.Client.OnMasterVolume -= this.OnMasterVolume;
			this.Client.OnMasterPower -= this.OnMasterPower;
			this.Client.OnReceiverNetworkInformation -= this.OnReceiverNetworkInformation;
		}
	}

	public class ConnectionBinder : Binder {
		public ConnectionBinder(ConnectionService service) => this.Service = service;

		public ConnectionService Service { get; }
	}
}