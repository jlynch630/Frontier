namespace Frontier {
	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Text;
	using Android.Util;
	using Android.Views;
	using Android.Widget;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Text;

	using PioneerApi;
	using Android.Graphics;

	public class ControlsFragment : AndroidX.Fragment.App.Fragment, IServiceConnector {
		private ImageButton PlayPause;
		private TextView Title;
		private TextView Subtitle;
		private TextView ExtraInfo;
		private ImageView AlbumArt;
		private ApiClient Client;

		private string LastAlbum = "";

		private string LastArtist = "";

		private bool? IsPlaying = null;

		private WebClient HttpClient = new WebClient();
		private ConnectionServiceConnection ServiceConnection;
		private ConnectionService Connection;

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			this.ServiceConnection = new ConnectionServiceConnection(this);
		}

		private void OnPlaybackState(object s, PlaybackState state) {
			this.Activity.RunOnUiThread(
				() => {
					this.Title.Text = state.Title ?? "";
					this.Subtitle.Text = $"{state.Artist ?? "No Artist"}{(state.Album == null ? "" : $" - {state.Album}")}";

					this.IsPlaying = state.PlayStatus?.PlayStatus == ApiClient.PlayStatus.Playing;
					this.PlayPause.SetImageResource(
						this.IsPlaying.Value
							? Resource.Drawable.round_pause_black_36
							: Resource.Drawable.round_play_arrow_black_36);
				});
		}

		private async void OnJacketArt(object s, JacketArtResolver.JacketArt art) {
			if (art == null) return;

			byte[] Data = art.Data;
			if (art.Url != null) {
				Data = await HttpClient.DownloadDataTaskAsync(art.Url);

				// now the server is written somewhat poorly in that it actually looks like HTTP 200 OK\n\nHeaders\n\nBody
				// so the client just assumes that the headers are part of the body
				// we need to get rid of those
				// Magic is FF D8 FF E0. Not like we'll see an FF in the headers, so we'll just skip to the first one
				int StartIndex = Array.IndexOf<byte>(Data, 0xFF);
				Data = Data.Skip(StartIndex).ToArray();
			}

			await System.IO.File.WriteAllBytesAsync(this.Activity.DataDir + "/img.jpg", Data);
			Bitmap Bitmap = await BitmapFactory.DecodeByteArrayAsync(Data, 0, Data.Length);
			this.Activity.RunOnUiThread(() => this.AlbumArt.SetImageBitmap(Bitmap));
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View RootView = inflater.Inflate(Resource.Layout.fragment_controls, container, false);

			this.PlayPause = RootView.FindViewById<ImageButton>(Resource.Id.play_pause);
			this.PlayPause.Enabled = true;
			this.PlayPause.Click += this.PlayPauseClick;

			this.Title = RootView.FindViewById<TextView>(Resource.Id.title);
			this.Subtitle = RootView.FindViewById<TextView>(Resource.Id.artist);
			this.ExtraInfo = RootView.FindViewById<TextView>(Resource.Id.extra_info);
			this.AlbumArt = RootView.FindViewById<ImageView>(Resource.Id.album_art);

			return RootView;
		}

		private async void PlayPauseClick(object sender, EventArgs e) {
			if (!IsPlaying.HasValue) return;

			if (IsPlaying == true)
				await this.Connection.Client.SendCommandAsync(CommandId.NetworkRemoteControl, "PAUSE");
			else
				await this.Connection.Client.SendCommandAsync(CommandId.NetworkRemoteControl, "PLAY");
		}

		public override void OnResume() {
			this.ServiceConnection.Connect(this.Activity);
			base.OnResume();
		}

		public override void OnPause() {
			this.ServiceConnection.Disconnect(this.Activity);
			base.OnPause();
		}

		public void OnServiceBound(ConnectionService service) {
			this.Connection = service;
			this.Connection.OnPlaybackStateChanged += OnPlaybackState;
			this.Connection.Client.OnJacketArtImage += OnJacketArt;
			this.OnPlaybackState(null, this.Connection.CurrentState);
			this.OnJacketArt(null, this.Connection.Art);
		}

		public void OnServiceUnbound() {
			this.Connection.OnPlaybackStateChanged -= OnPlaybackState;
			this.Connection.Client.OnJacketArtImage -= OnJacketArt;
			this.Connection = null;
		}
	}
}