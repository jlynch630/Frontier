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
	using System.Globalization;
	using System.Linq;
	using System.Text;

	internal class InputListAdapter : RecyclerView.Adapter {
		private List<Selector> Dataset;

		public InputListAdapter(List<Selector> dataset) => this.Dataset = dataset;

		public event EventHandler<Selector> ItemClick;

		public List<Selector> Selectors {
			get => this.Dataset;
			set {
				this.Dataset = value;
				this.NotifyDataSetChanged();
			}
		}

		private class InputViewHolder : RecyclerView.ViewHolder {
			public ImageView Icon { get; private set; }

			public TextView Text { get; private set; }

			public InputViewHolder(View itemView)
				: base(itemView) {
				this.Icon = itemView.FindViewById<ImageView>(Resource.Id.input_icon);
				this.Text = itemView.FindViewById<TextView>(Resource.Id.input_text);
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
			InputViewHolder InputHolder = (InputViewHolder)holder;
			Selector Item = this.Dataset[position];
			InputHolder.Icon.SetImageResource(IconIdMapper.IconIdMap[Int32.Parse(Item.IconId ?? "7", NumberStyles.HexNumber)]);
			InputHolder.Text.Text = Item.Name;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
			View ItemView = LayoutInflater.From(parent.Context).
				Inflate(Resource.Layout.list_item_input, parent, false);

			// Create a ViewHolder to hold view references inside the CardView:
			InputViewHolder ViewHolder = new InputViewHolder(ItemView);
			ViewHolder.ItemView.Click +=
				(s, e) => this.ItemClick?.Invoke(this, this.Dataset[ViewHolder.LayoutPosition]);
			return ViewHolder;
		}


		public override int ItemCount => this.Dataset.Count;
	}
}