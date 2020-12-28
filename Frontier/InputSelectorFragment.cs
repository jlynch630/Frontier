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

	using AndroidX.RecyclerView.Widget;

	public class InputSelectorFragment : BoundFragment {
		private RecyclerView RecyclerView;
		private InputListAdapter Adapter;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View RootView = inflater.Inflate(Resource.Layout.fragment_inputselect, container, false);
			this.RecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
			this.RecyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
			this.Adapter = new InputListAdapter(new List<Selector>());
			this.Adapter.ItemClick += this.AdapterItemClick;
			this.RecyclerView.SetAdapter(this.Adapter);
			return RootView;
		}

		private async void AdapterItemClick(object sender, Selector e) {
			await this.Connection.Client.SendCommandAsync(CommandId.InputSelection, e.Id);
			((IFragmentChangeCallback)this.Activity).RequestHome();
		}

		protected override void AttachEvents() {
			this.Connection.Client.OnReceiverNetworkInformation += this.OnReceiverNetworkInformation;
			this.OnReceiverNetworkInformation(null, this.Connection.ReceiverInfo);
		}

		private void OnReceiverNetworkInformation(object sender, ReceiverInformationResponse info) {
			if (info == null) return;
			this.Adapter.Selectors = info.Device.SelectorList.Selectors;
		}

		protected override void RemoveEvents() {
			this.Connection.Client.OnReceiverNetworkInformation -= this.OnReceiverNetworkInformation;
		}
	}
}