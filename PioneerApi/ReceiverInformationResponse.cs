// -----------------------------------------------------------------------
// <copyright file="ReceiverInformationResponse.cs" company="John Lynch">
//   This file is licensed under the MIT license
//   Copyright (c) 2020 John Lynch
// </copyright>
// -----------------------------------------------------------------------

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Response));
// using (stringReader reader = new stringReader(xml))
// {
//    var test = (Response)serializer.Deserialize(reader);
// }

namespace PioneerApi {
	using System.Collections.Generic;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "netservice")]
	public class NetService {
		[XmlAttribute(AttributeName = "account")]
		public string Account { get; set; }

		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "password")]
		public string Password { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "netservicelist")]
	public class NetServiceList {
		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }

		[XmlElement(ElementName = "netservice")]
		public List<NetService> NetService { get; set; }
	}

	[XmlRoot(ElementName = "zone")]
	public class Zone {
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }

		[XmlAttribute(AttributeName = "volmax")]
		public string Volmax { get; set; }

		[XmlAttribute(AttributeName = "volstep")]
		public string Volstep { get; set; }
	}

	[XmlRoot(ElementName = "zonelist")]
	public class ZoneList {
		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }

		[XmlElement(ElementName = "zone")]
		public List<Zone> Zone { get; set; }
	}

	[XmlRoot(ElementName = "selector")]
	public class Selector {
		[XmlAttribute(AttributeName = "iconid")]
		public string IconId { get; set; }

		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }

		[XmlAttribute(AttributeName = "zone")]
		public string Zone { get; set; }
	}

	[XmlRoot(ElementName = "selectorlist")]
	public class SelectorList {
		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }

		[XmlElement(ElementName = "selector")]
		public List<Selector> Selectors { get; set; }
	}

	[XmlRoot(ElementName = "preset")]
	public class Preset {
		[XmlAttribute(AttributeName = "band")]
		public string Band { get; set; }

		[XmlAttribute(AttributeName = "freq")]
		public string Freq { get; set; }

		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "presetlist")]
	public class PresetList {
		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }

		[XmlElement(ElementName = "preset")]
		public List<Preset> Preset { get; set; }
	}

	[XmlRoot(ElementName = "control")]
	public class Control {
		[XmlAttribute(AttributeName = "code")]
		public string Code { get; set; }

		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		[XmlAttribute(AttributeName = "max")]
		public string Max { get; set; }

		[XmlAttribute(AttributeName = "min")]
		public string Min { get; set; }

		[XmlAttribute(AttributeName = "position")]
		public string Position { get; set; }

		[XmlAttribute(AttributeName = "step")]
		public string Step { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }

		[XmlAttribute(AttributeName = "zone")]
		public string Zone { get; set; }
	}

	[XmlRoot(ElementName = "controllist")]
	public class ControlList {
		[XmlElement(ElementName = "control")]
		public List<Control> Control { get; set; }

		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }
	}

	[XmlRoot(ElementName = "function")]
	public class Function {
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "functionlist")]
	public class FunctionList {
		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }

		[XmlElement(ElementName = "function")]
		public List<Function> Function { get; set; }
	}

	[XmlRoot(ElementName = "tuner")]
	public class Tuner {
		[XmlAttribute(AttributeName = "band")]
		public string Band { get; set; }

		[XmlAttribute(AttributeName = "max")]
		public string Max { get; set; }

		[XmlAttribute(AttributeName = "min")]
		public string Min { get; set; }

		[XmlAttribute(AttributeName = "step")]
		public string Step { get; set; }
	}

	[XmlRoot(ElementName = "tuners")]
	public class Tuners {
		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }

		[XmlElement(ElementName = "tuner")]
		public List<Tuner> Tuner { get; set; }
	}

	[XmlRoot(ElementName = "device")]
	public class Device {
		[XmlElement(ElementName = "brand")]
		public string Brand { get; set; }

		[XmlElement(ElementName = "category")]
		public string Category { get; set; }

		[XmlElement(ElementName = "netservicelist")]
		public NetServiceList NetServiceList { get; set; }

		[XmlElement(ElementName = "zonelist")]
		public ZoneList ZoneList { get; set; }

		[XmlElement(ElementName = "selectorlist")]
		public SelectorList SelectorList { get; set; }

		[XmlElement(ElementName = "presetlist")]
		public PresetList PresetList { get; set; }

		[XmlElement(ElementName = "controllist")]
		public ControlList ControlList { get; set; }

		[XmlElement(ElementName = "year")]
		public string Year { get; set; }

		[XmlElement(ElementName = "model")]
		public string Model { get; set; }

		[XmlElement(ElementName = "destination")]
		public string Destination { get; set; }

		[XmlElement(ElementName = "modeliconurl")]
		public string ModelIconUrl { get; set; }

		[XmlElement(ElementName = "friendlyname")]
		public string FriendlyName { get; set; }

		[XmlElement(ElementName = "firmwareversion")]
		public string FirmwareVersion { get; set; }

		[XmlElement(ElementName = "functionlist")]
		public FunctionList FunctionList { get; set; }

		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }

		[XmlElement(ElementName = "tuners")]
		public Tuners Tuners { get; set; }
	}

	[XmlRoot(ElementName = "response")]
	public class ReceiverInformationResponse {
		[XmlElement(ElementName = "device")]
		public Device Device { get; set; }

		[XmlAttribute(AttributeName = "status")]
		public string Status { get; set; }
	}
}