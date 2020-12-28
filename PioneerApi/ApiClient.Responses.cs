namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;

	public partial class ApiClient {
		public enum PlayStatus {
			Playing,
			Paused,
			Stopped,
			FastForwarding,
			Rewinding,
			End
		}

		public enum ShuffleRepeatStatus {
			Disabled,
			All,
			Off,
			Folder,
			Album,
			Single
		}

		public class PlayInfo {
			public PlayStatus PlayStatus { get; private set; }
			public ShuffleRepeatStatus ShuffleStatus { get; private set; }
			public ShuffleRepeatStatus RepeatStatus { get; private set; }

			public static PlayInfo Parse(string serialized) {
				if (serialized.Length != 3) return null;
				char PlayStatusChar = serialized[0];
				char RepeatStatusChar = serialized[1];
				char ShuffleStatusChar = serialized[2];
				PlayStatus PlayStatus;
				ShuffleRepeatStatus ShuffleStatus;
				ShuffleRepeatStatus RepeatStatus;

				switch (PlayStatusChar) {
					case 'S':
						PlayStatus = PlayStatus.Stopped;
						break;

					case 'P':
						PlayStatus = PlayStatus.Playing;
						break;

					case 'p':
						PlayStatus = PlayStatus.Paused;
						break;

					case 'F':
						PlayStatus = PlayStatus.FastForwarding;
						break;

					case 'R':
						PlayStatus = PlayStatus.Rewinding;
						break;

					default:
						PlayStatus = PlayStatus.End;
						break;
				}

				switch (RepeatStatusChar) {
					case '-':
						RepeatStatus = ShuffleRepeatStatus.Off;
						break;
					case 'R':
						RepeatStatus = ShuffleRepeatStatus.All;
						break;
					case 'F':
						RepeatStatus = ShuffleRepeatStatus.Folder;
						break;
					case '1':
						RepeatStatus = ShuffleRepeatStatus.Single;
						break;
					default:
						RepeatStatus = ShuffleRepeatStatus.Disabled;
						break;
				}

				switch (ShuffleStatusChar) {
					case '-':
						ShuffleStatus = ShuffleRepeatStatus.Off;
						break;
					case 'S':
						ShuffleStatus = ShuffleRepeatStatus.All;
						break;
					case 'F':
						ShuffleStatus = ShuffleRepeatStatus.Folder;
						break;
					case 'A':
						ShuffleStatus = ShuffleRepeatStatus.Album;
						break;
					default:
						ShuffleStatus = ShuffleRepeatStatus.Disabled;
						break;
				}

				return new PlayInfo {
					                    PlayStatus = PlayStatus,
					                    RepeatStatus = RepeatStatus,
					                    ShuffleStatus = ShuffleStatus
				                    };
			}
		}

		public enum ServiceType {
			DLNA = 0x00,
			Favorite = 0x01,
			VTuner = 0x02,
			SiriusXM = 0x03,
			Pandora = 0x04,
			Rhapsody = 0x05,
			LastFm = 0x06,
			Napster = 0x07,
			Slacker = 0x08,
			Mediafly =  0x09,
			Spotify = 0x0A,
			AUPEO = 0x0B,
			Radiko = 0x0C,
			EOnkyo = 0x0D,
			TuneIn = 0x0E,
			MP3Tunes = 0x0F,
			Simfy = 0x10,
			HomeMedia = 0x11,
			Deezer = 0x12,
			IHeartRadio = 0x13,
			UsbFront = 0xF0,
			UsbRear = 0xF1,
			InternetRadio = 0xF2,
			Net = 0xF3,
			None = 0xFF
		}

		public enum ListUIType {
			List = 0,
			Menu = 1,
			Playback = 2,
			Popup = 3,
			Keyboard = 4,
			MenuList = 5,
			Unknown = 6
		}

		public class NetworkListTitleInfo {
			private NetworkListTitleInfo(ServiceType service, ListUIType uiType, int layer, int cursorPosition, int layerIndex, ServiceType icon, int status, string title, int itemCount) {
				this.Service = service;
				this.UIType = uiType;
				this.Layer = layer;
				this.CursorPosition = cursorPosition;
				this.LayerIndex = layerIndex;
				this.Icon = icon;
				this.Status = status;
				this.Title = title;
				this.ItemCount = itemCount;
			}

			public ServiceType Service { get; }
			public ListUIType UIType { get; }
			public int Layer { get; }
			public int ItemCount { get; }
			public int CursorPosition { get; }
			public int LayerIndex { get; }
			public ServiceType Icon { get; }
			public int Status { get; }
			public string Title { get; }

			public static NetworkListTitleInfo Parse(string data) {
				// very simple this one
				ServiceType Service = (ServiceType)Int32.Parse(data.Substring(0, 2), NumberStyles.HexNumber);
				ListUIType UI = (ListUIType)Int32.Parse(data.Substring(2, 1), NumberStyles.HexNumber);
				int Layer = Int32.Parse(data.Substring(3, 1), NumberStyles.HexNumber);

				int CursorPosition = Int32.Parse(data.Substring(4, 4), NumberStyles.HexNumber);
				int ItemCount = Int32.Parse(data.Substring(8, 4), NumberStyles.HexNumber);
				int LayerIndex = Int32.Parse(data.Substring(12, 2), NumberStyles.HexNumber);
				ServiceType Icon = (ServiceType)Int32.Parse(data.Substring(18, 2), NumberStyles.HexNumber);
				int Status = Int32.Parse(data.Substring(20, 2), NumberStyles.HexNumber);
				string Title = data.Substring(22);

				return new NetworkListTitleInfo(
					Service,
					UI,
					Layer,
					CursorPosition,
					LayerIndex,
					Icon,
					Status,
					Title,
					ItemCount);
			}
		}
	}
}
