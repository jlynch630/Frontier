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

	public class ConnectionServiceConnection : Java.Lang.Object, IServiceConnection {
		private readonly IServiceConnector Connector;
		public ConnectionBinder Binder { get; private set; }

		public ConnectionServiceConnection(IServiceConnector connector) {
			this.Connector = connector;
		}

		public void Connect(Context context) {
			Intent StartConnection = new Intent(context, typeof(ConnectionService));
			context.BindService(StartConnection, this, Bind.AutoCreate);
		}

		public void Disconnect(Context context) {
			context.UnbindService(this);
		}

		public void OnServiceConnected(ComponentName? name, IBinder? service) {
			this.Binder = (ConnectionBinder)service;
			if (this.Binder == null) throw new NullReferenceException("my null reference exception1");
			this.Connector.OnServiceBound(this.Binder.Service);
		}

		public void OnServiceDisconnected(ComponentName? name) {
			this.Connector.OnServiceUnbound();
		}
	}

	public interface IServiceConnector {
		void OnServiceBound(ConnectionService service);

		void OnServiceUnbound();	
	}
}