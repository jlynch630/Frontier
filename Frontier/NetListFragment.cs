namespace Frontier {
	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Views;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using PioneerApi;
	using Android.Util;
	using Android.Widget;

	using AndroidX.RecyclerView.Widget;

	using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

	public class NetListFragment : BoundFragment {
		private ListView ListView;
		
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View RootView = inflater.Inflate(Resource.Layout.fragment_netlist, container, false);
			this.ListView = RootView.FindViewById<ListView>(Resource.Id.listview);
			this.ListView.ItemClick += this.ItemClicked;
			RootView.FindViewById<Toolbar>(Resource.Id.toolbar).Title = "Select Service";
			return RootView;
		}

		private async void ItemClicked(object sender, AdapterView.ItemClickEventArgs e) {
			int Index = e.Position;

			string ServiceId = this.Connection.ReceiverInfo.Device.NetServiceList.NetService[Index].Id;
			await this.Connection.Client.SendCommandAsync(
				CommandId.NetworkServiceSelection,
				$"{ServiceId}0");
			((IFragmentChangeCallback)this.Activity).RequestHome();
		}

		protected override void AttachEvents() {
			this.Connection.Client.OnReceiverNetworkInformation += this.OnReceiverInfo;
			this.OnReceiverInfo(null, this.Connection.ReceiverInfo);
		}

		private void OnReceiverInfo(object sender, ReceiverInformationResponse e) {
			string[] ItemText = e.Device.NetServiceList.NetService.Select(i => i.Name).ToArray();

			this.Activity.RunOnUiThread(
				() => {
					this.ListView.Adapter = new ArrayAdapter<string>(
						this.Activity,
						Android.Resource.Layout.SimpleListItem1,
						ItemText);
				});
		}

		protected override void RemoveEvents() {
			this.Connection.Client.OnReceiverNetworkInformation -= this.OnReceiverInfo;
		}
	}
}