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

	public class ListFragment : BoundFragment {
		private string Title;

		private int Layer;

		private int Count;

		private ListView ListView;

		private static int SequenceNumber = 0;

		private List<Item> ListItems;

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			this.Title = this.Arguments.GetString("title", "List");
			this.Count = this.Arguments.GetInt("count", 0);
			this.Layer = this.Arguments.GetInt("layer", 0);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View RootView = inflater.Inflate(Resource.Layout.fragment_netlist, container, false);
			this.ListView = RootView.FindViewById<ListView>(Resource.Id.listview);
			this.ListView.ItemClick += this.ItemClicked;
			RootView.FindViewById<Toolbar>(Resource.Id.toolbar).Title = this.Title;
			return RootView;
		}

		private async void ItemClicked(object sender, AdapterView.ItemClickEventArgs e) {
			int Index = e.Position;
			/**
			 * NLA I 02 0000 ----
				I -> select
				02 -> active level
				0000 -> selection index
				---- -> hardcoded
			 */

			await this.Connection.Client.SendCommandAsync(
				CommandId.NetworkListInfoAll,
				$"I{this.Layer:X2}{Index:X4}----");
			((IFragmentChangeCallback)this.Activity).RequestHome();
		}

		protected override void AttachEvents() {
			this.RequestListData();
			this.Connection.Client.OnNetworkListInfoAll += this.OnNetworkListInfo;
		}

		private async void RequestListData() {
			/*
			 * "specify to get the listed data (from Network Control Only)
				zzzz -> sequence number (0000-FFFF)
				ll -> number of layer (00-FF)
				xxxx -> index of start item (0000-FFFF : 1st to 65536th Item [4 HEX digits] )
				yyyy -> number of items (0000-FFFF : 1 to 65536 Items [4 HEX digits] )"
			 */
			await this.Connection.Client.SendCommandAsync(
				CommandId.NetworkListInfoAll,
				$"L{ListFragment.SequenceNumber++:X4}{this.Layer:X2}0000{this.Count:X4}");
		}

		private void OnNetworkListInfo(object sender, ListItemsResponse e) {
			this.ListItems = e.Items;
			string[] ItemText = e.Items.Select(i => i.Title).ToArray();

			this.Activity.RunOnUiThread(
				() => {
					this.ListView.Adapter = new ArrayAdapter<string>(
						this.Activity,
						Android.Resource.Layout.SimpleListItem1,
						ItemText);
				});
		}

		protected override void RemoveEvents() {
			this.Connection.Client.OnNetworkListInfoAll -= this.OnNetworkListInfo;
		}
	}
}