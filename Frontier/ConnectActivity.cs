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
	using AndroidX.AppCompat.App;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	using AndroidX.RecyclerView.Widget;

	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class ConnectActivity : AppCompatActivity, IServiceConnector {
		private ConnectionServiceConnection ServiceConnection;

		private ConnectionService Connection;

		private EditText IpAddressText;
		private Button ConnectButton;
		private RecyclerView DiscoveryList;
		private DiscoveredDeviceAdapter Adapter;

		private ISharedPreferences Preferences;
		private DiscoveryService Discoverer = new DiscoveryService();

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_connect);
			this.Preferences = this.GetSharedPreferences("app", FileCreationMode.Private);
			
			this.IpAddressText = this.FindViewById<EditText>(Resource.Id.ip_text);
			this.ConnectButton = this.FindViewById<Button>(Resource.Id.connect_button);
			this.ConnectButton.Click += this.ConnectButtonClick;
			this.DiscoveryList = this.FindViewById<RecyclerView>(Resource.Id.recyclerView);
			this.DiscoveryList.SetLayoutManager(new LinearLayoutManager(this));
			this.Adapter = new DiscoveredDeviceAdapter();
			this.Adapter.ItemClick += this.DiscoveredDeviceClick;
			this.DiscoveryList.SetAdapter(this.Adapter);

			this.ServiceConnection = new ConnectionServiceConnection(this);
			this.ServiceConnection.Connect(this);

			Discoverer.DeviceFound += this.DeviceDiscovered;
		}

		private async void DiscoveredDeviceClick(object sender, DiscoveredDevice device) {
			await this.Connect($"{device.IpAddress}:{device.PortNumber}");
		}

		private void DeviceDiscovered(object sender, DiscoveredDevice device) {
			this.Adapter.Dataset.Add(device);
		}

		private async void ConnectButtonClick(object sender, EventArgs e) {
			if (this.Connection == null) {
				Toast.MakeText(this, "Error. Try again.", ToastLength.Short).Show();
				return;
			}

			string? Text = this.IpAddressText.Text;

			if (Text == null || !Regex.IsMatch(Text, "\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}(:\\d{2,5})?")) {
				Toast.MakeText(this, "Invalid IP Address", ToastLength.Short).Show();
				return;
			}

			await this.Connect(Text);
		}

		private async Task Connect(string host) {
			try {
				this.ConnectButton.Enabled = false;
				await this.Connection.Connect(host);
				this.Preferences.Edit().PutString("ip", host).Apply();
				this.StartActivity(new Intent(this, typeof(MainActivity)));
			} catch {
				Toast.MakeText(this, "Failed to connect", ToastLength.Long).Show();
			}

			this.ConnectButton.Enabled = true;
		}

		protected override void OnResume() {
			this.Discoverer.StartDiscovery();
			base.OnResume();
		}

		protected override void OnPause() {
			this.Discoverer.StopDiscovery();
			base.OnResume();
		}

		protected override void OnDestroy() {
			this.ServiceConnection.Disconnect(this);
			base.OnDestroy();
		}

		public async void OnServiceBound(ConnectionService service) {
			this.Connection = service;

			string Existing = this.Preferences.GetString("ip", null);
			if (Existing == null) {
				this.Discoverer.StartDiscovery();
				return;
			}

			try {
				await this.Connection.Connect(Existing);
				this.StartActivity(new Intent(this, typeof(MainActivity)));
			}
			catch {
				Toast.MakeText(this, "Failed to connect", ToastLength.Long).Show();
				this.Discoverer.StartDiscovery();
			}
		}

		public void OnServiceUnbound() {
			this.Connection = null;
		}
	}
}