// -----------------------------------------------------------------------
// <copyright file="ListItemsResponse.cs" company="John Lynch">
//   This file is licensed under the MIT license
//   Copyright (c) 2020 John Lynch
// </copyright>
// -----------------------------------------------------------------------

namespace PioneerApi {
	using System.Collections.Generic;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "response")]
	public class ListItemsResponse {
		public List<Item> Items => this.Response.Items;

		[XmlElement(ElementName = "items")]
		public ListItems Response { get; set; }

		[XmlAttribute(AttributeName = "status")]
		public string Status { get; set; }
	}

	[XmlRoot(ElementName = "item")]
	public class Item {
		[XmlAttribute(AttributeName = "iconid")]
		public string IconId { get; set; }

		[XmlAttribute(AttributeName = "icontype")]
		public string IconType { get; set; }

		[XmlAttribute(AttributeName = "title")]
		public string Title { get; set; }
	}

	[XmlRoot(ElementName = "items")]
	public class ListItems {
		[XmlElement(ElementName = "item")]
		public List<Item> Items { get; set; }

		[XmlAttribute(AttributeName = "offset")]
		public string Offset { get; set; }

		[XmlAttribute(AttributeName = "totalitems")]
		public string TotalItems { get; set; }
	}
}