namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices.ComTypes;
	using System.Text;

	public class JacketArtResolver {
		public event EventHandler<JacketArt> OnJacketArtImage;

		private JacketArt CurrentArt;

		public JacketArtResolver(ApiClient client) {
			client.OnNetworkJacketArt += this.OnJacketArtPacket;
		}

		private void OnJacketArtPacket(object sender, string jacketArt) {
			//"tpxxxxxxxxxxxx"
			/*
			 *"NET/USB Jacket Art/Album Art Data
				t-> Image type 0:BMP, 1:JPEG, 2:URL, n:No Image
				p-> Packet flag 0:Start, 1:Next, 2:End, -:not used
				xxxxxxxxxxxxxx -> Jacket/Album Art Data (variable length, 1024 ASCII HEX letters max)"
			*
			 */
			if (jacketArt[1] == '-' && jacketArt[0] == '2') {
				// just a url
				string Url = jacketArt.Substring(2);
				this.OnJacketArtImage?.Invoke(this, new JacketArt() { Url = Url, Type = JacketArtType.URL });
				return;
			}

			if (jacketArt[1] == '0') {
				this.CurrentArt = new JacketArt();
				int ImageType = Int32.Parse(jacketArt[0].ToString());
				this.CurrentArt.Type = (JacketArtType)ImageType;
			}

			if (this.CurrentArt == null) return;

			string HexData = jacketArt.Substring(2);
			byte[] DataBytes = new byte[HexData.Length / 2];
			for (int i = 0; i < HexData.Length; i += 2) {
				DataBytes[i / 2] = Convert.ToByte(HexData.Substring(i, 2), 16); 
			}
			this.CurrentArt.Data = this.CurrentArt.Data.Concat(DataBytes).ToArray();
			if (jacketArt[1] != '2') return;

			this.OnJacketArtImage?.Invoke(this, this.CurrentArt);
			this.CurrentArt = null;
		}

		public class JacketArt {
			public JacketArtType Type { get; set; }

			public byte[] Data { get; set; } = new byte[0];

			public string Url { get; set; }
		}

		public enum JacketArtType {
			BMP,
			JPEG,
			URL,
			None
		}
	}
}
