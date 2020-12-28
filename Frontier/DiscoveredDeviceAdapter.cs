namespace Frontier {
	using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Runtime;
	using Android.Views;
	using Android.Widget;
	using AndroidX.RecyclerView.Widget;
	using PioneerApi;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.Globalization;
	using System.Linq;
	using System.Text;

	internal class DiscoveredDeviceAdapter : RecyclerView.Adapter {
		public ObservableCollection<DiscoveredDevice> Dataset { get; } = new ObservableCollection<DiscoveredDevice>();

		public DiscoveredDeviceAdapter() {
			this.Dataset.CollectionChanged += this.DatasetChanged;
		}

		private void DatasetChanged(object sender, NotifyCollectionChangedEventArgs e) {
			this.NotifyDataSetChanged();
		}

		public event EventHandler<DiscoveredDevice> ItemClick;

		private class DeviceViewHolder : RecyclerView.ViewHolder {
			public TextView Model { get; private set; }

			public TextView Address { get; private set; }

			public DeviceViewHolder(View itemView)
				: base(itemView) {
				this.Model = itemView.FindViewById<TextView>(Resource.Id.model_name);
				this.Address = itemView.FindViewById<TextView>(Resource.Id.address);
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			DeviceViewHolder DeviceHolder = (DeviceViewHolder)holder;
			DiscoveredDevice Item = this.Dataset[position];
			DeviceHolder.Model.Text = Item.ModelName;
			DeviceHolder.Address.Text = Item.IpAddress;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			View ItemView = LayoutInflater.From(parent.Context).
				Inflate(Resource.Layout.list_item_device, parent, false);

			// Create a ViewHolder to hold view references inside the CardView:
			DeviceViewHolder ViewHolder = new DeviceViewHolder(ItemView);
			ViewHolder.ItemView.Click +=
				(s, e) => this.ItemClick?.Invoke(this, this.Dataset[ViewHolder.LayoutPosition]);
			return ViewHolder;
		}


		public override int ItemCount => this.Dataset.Count;
	}
}