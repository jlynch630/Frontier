using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace Frontier
{
	using Android.Content;
	using Android.Views;
	using PioneerApi;
	using System;

	using Android.Util;
	using AndroidX.Fragment.App;

	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : FragmentActivity, IServiceConnector, IFragmentChangeCallback {
		private ConnectionServiceConnection ServiceConnection;

		private ConnectionService Connection;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            this.ServiceConnection = new ConnectionServiceConnection(this);
            this.ServiceConnection.Connect(this);
			this.RequestHome();
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

		protected override void OnDestroy() {
			this.ServiceConnection.Disconnect(this);
			base.OnDestroy();
		}

		public override bool DispatchKeyEvent(KeyEvent e) {
			Keycode KeyCode = e.KeyCode;
			switch (KeyCode) {
				case Keycode.VolumeUp:
					this.Connection?.Client.SendCommandAsync(CommandId.MasterVolume, "UP");
					return true;
				case Keycode.VolumeDown:
					this.Connection?.Client.SendCommandAsync(CommandId.MasterVolume, "DOWN");
					return true;
				default:
					return base.DispatchKeyEvent(e);
			}
		}

		public void OnServiceBound(ConnectionService service) {
			this.Connection = service;
			this.Connection.Client.OnNetworkListTitleInfo += this.OnNetworkListTitleInfo;
		}

		private void OnNetworkListTitleInfo(object sender, ApiClient.NetworkListTitleInfo info) {
			// when we receive the title, it's time to bring up a new list
			Bundle Data = new Bundle();
			if (info.Service == ApiClient.ServiceType.Net) {
				this.SetFragment(new NetListFragment());
				return;
			}

			if (info.LayerIndex == 0) return;
			if (info.UIType == ApiClient.ListUIType.Unknown) return;
			if (info.ItemCount == 0) return;

			Data.PutString("title", info.Title);
			Data.PutInt("count", info.ItemCount);
			Data.PutInt("layer", info.LayerIndex);
			Fragment ListFragment = new ListFragment();
			ListFragment.Arguments = Data;
			this.SetFragment(ListFragment);
		}

		public void OnServiceUnbound() {
			this.Connection.Client.OnNetworkListTitleInfo -= this.OnNetworkListTitleInfo;
			this.Connection = null;
		}

		public void RequestHome() {
			this.SetFragment(new HomeFragment());
		}

		public void RequestInputSelect() {
			this.SetFragment(new InputSelectorFragment());
		}

		private void SetFragment(Fragment target) {
			FragmentTransaction Transaction = this.SupportFragmentManager.BeginTransaction();

			Transaction.Replace(Resource.Id.fragment_container, target);
			Transaction.AddToBackStack(null);
			Transaction.Commit();
		}
    }

	internal interface IFragmentChangeCallback {
		void RequestHome();

		void RequestInputSelect();
	}
}