namespace Frontier {
	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using PioneerApi;
	using Android.Util;

	public class HomeFragment : BoundFragment {
		private Button PowerToggleButton;
		private Button SelectInputButton;
		private TextView VolumeIndicator;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View RootView = inflater.Inflate(Resource.Layout.fragment_home, container, false);

			this.PowerToggleButton = RootView.FindViewById<Button>(Resource.Id.power_toggle);
			this.SelectInputButton = RootView.FindViewById<Button>(Resource.Id.input_select);
			this.VolumeIndicator = RootView.FindViewById<TextView>(Resource.Id.volume_indicator);

			this.PowerToggleButton.Click += this.PowerToggleButtonClick;
			this.SelectInputButton.Click += this.SelectInputButtonClick;
			return RootView;
		}

		private void SelectInputButtonClick(object sender, EventArgs e) {
			((IFragmentChangeCallback)this.Activity).RequestInputSelect();
		}

		private async void PowerToggleButtonClick(object sender, System.EventArgs e) {
			await this.Connection.Client.SendCommandAsync(
				CommandId.MasterPower,
				this.Connection.IsPoweredOn ? "00" : "01");

		}

		private void OnMasterVolume(object sender, int volume) {
			this.Activity.RunOnUiThread(() => this.VolumeIndicator.Text = $"Volume: {volume}");
		}

		private void OnMasterPower(object sender, bool isOn) {
			this.Activity.RunOnUiThread(() => this.PowerToggleButton.Text = $"Power {(isOn ? "Off" : "On")}");
		}

		protected override void AttachEvents() {
			this.Connection.Client.OnMasterPower += this.OnMasterPower;
			this.Connection.Client.OnMasterVolume += this.OnMasterVolume;
			this.OnMasterPower(null, this.Connection.IsPoweredOn);
			this.OnMasterVolume(null, this.Connection.MasterVolume);
		}

		protected override void RemoveEvents() {
			this.Connection.Client.OnMasterPower -= this.OnMasterPower;
			this.Connection.Client.OnMasterVolume -= this.OnMasterVolume;
		}
	}
}