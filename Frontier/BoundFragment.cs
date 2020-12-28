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

	public abstract class BoundFragment : AndroidX.Fragment.App.Fragment, IServiceConnector {
		private ConnectionServiceConnection ServiceConnection;
		protected ConnectionService Connection { get; private set; }

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			this.ServiceConnection = new ConnectionServiceConnection(this);
			this.ServiceConnection.Connect(this.Activity);
		}

		public override void OnDestroy() {
			this.ServiceConnection.Disconnect(this.Activity);
			base.OnDestroy();
		}

		public void OnServiceBound(ConnectionService service) {
			this.Connection = service;
			this.AttachEvents();
		}

		public void OnServiceUnbound() {
			this.RemoveEvents();
			this.Connection = null;
		}

		protected virtual void AttachEvents() { }

		protected virtual void RemoveEvents() { }
	}
}