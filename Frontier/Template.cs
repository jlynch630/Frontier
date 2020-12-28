//namespace Frontier {
//	using Android.App;
//	using Android.Content;
//	using Android.OS;
//	using Android.Runtime;
//	using Android.Text;
//	using Android.Util;
//	using Android.Views;
//	using Android.Widget;
//	using System;
//	using System.Collections.Generic;
//	using System.Linq;
//	using System.Net;
//	using System.Text;

//	using PioneerApi;
//	using Android.Graphics;

//	public class InputSelectorFragment : AndroidX.Fragment.App.Fragment, IServiceConnector {
//		private ConnectionServiceConnection ServiceConnection;
//		private ConnectionService Connection;

//		public override void OnCreate(Bundle savedInstanceState) {
//			base.OnCreate(savedInstanceState);
//			this.ServiceConnection = new ConnectionServiceConnection(this);
//		}

//		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
//			View RootView = inflater.Inflate(Resource.Layout.fragment_controls, container, false);

//			return RootView;
//		}

//		public override void OnResume() {
//			this.ServiceConnection.Connect(this.Activity);
//			base.OnResume();
//		}

//		public override void OnPause() {
//			this.ServiceConnection.Disconnect(this.Activity);
//			base.OnPause();
//		}

//		public void OnServiceBound(ConnectionService service) {
//			this.Connection = service;
//		}

//		public void OnServiceUnbound() {
//			this.Connection = null;
//		}
//	}
//}