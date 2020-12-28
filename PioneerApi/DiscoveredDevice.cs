namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Text;

	public class DiscoveredDevice {
		public string ModelName { get; set; }
		public int PortNumber { get; set; }
		public string IpAddress { get; set; }
		public string MacAddress { get; set; }

		public static DiscoveredDevice Parse(ISCPMessage message, EndPoint from) {
			string[] Parts = message.Parameters.Split('/');
			return new DiscoveredDevice {
				                            ModelName = Parts[0],
				                            PortNumber = Int32.Parse(Parts[1]),
				                            MacAddress = Parts[3],
				                            IpAddress = ((IPEndPoint)from).Address.ToString()
			                            };
		}
	}
}
