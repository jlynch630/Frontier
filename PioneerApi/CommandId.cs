namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class CommandId {
		/// <summary>
		///     Code for the 'Master Power' command
		/// </summary>
		public const string MasterPower = "PWR";

		/// <summary>
		///     Code for the 'Audio Muting' command
		/// </summary>
		public const string AudioMuting = "AMT";

		/// <summary>
		///     Code for the 'Speaker A Control' command
		/// </summary>
		public const string SpeakerAControl = "SPA";

		/// <summary>
		///     Code for the 'Speaker B Control' command
		/// </summary>
		public const string SpeakerBControl = "SPB";

		/// <summary>
		///     Code for the 'Speaker Layout' command
		/// </summary>
		public const string SpeakerLayout = "SPL";

		/// <summary>
		///     Code for the 'Master Volume' command
		/// </summary>
		public const string MasterVolume = "MVL";

		/// <summary>
		///     Code for the 'Tone Front' command
		/// </summary>
		public const string ToneFront = "TFR";

		/// <summary>
		///     Code for the 'Tone Front Wide' command
		/// </summary>
		public const string ToneFrontWide = "TFW";

		/// <summary>
		///     Code for the 'Tone Front High' command
		/// </summary>
		public const string ToneFrontHigh = "TFH";

		/// <summary>
		///     Code for the 'Tone Center' command
		/// </summary>
		public const string ToneCenter = "TCT";

		/// <summary>
		///     Code for the 'Tone Surround' command
		/// </summary>
		public const string ToneSurround = "TSR";

		/// <summary>
		///     Code for the 'Tone Surround Back' command
		/// </summary>
		public const string ToneSurroundBack = "TSB";

		/// <summary>
		///     Code for the 'Tone Subwoofer' command
		/// </summary>
		public const string ToneSubwoofer = "TSW";

		/// <summary>
		///     Code for the 'Phase-Matching Bass Control' command
		/// </summary>
		public const string PhaseMatchingBassControl = "PMB";

		/// <summary>
		///     Code for the 'Sleep Control' command
		/// </summary>
		public const string SleepControl = "SLP";

		/// <summary>
		///     Code for the 'Speaker Level Calibration' command
		/// </summary>
		public const string SpeakerLevelCalibration = "SLC";

		/// <summary>
		///     Code for the 'Subwoofer Level Control' command
		/// </summary>
		public const string SubwooferLevelControl = "SWL";

		/// <summary>
		///     Code for the 'Subwoofer 2 Level Control' command
		/// </summary>
		public const string Subwoofer2LevelControl = "SW2";

		/// <summary>
		///     Code for the 'Center Level Control' command
		/// </summary>
		public const string CenterLevelControl = "CTL";

		/// <summary>
		///     Code for the 'Display Information Control' command
		/// </summary>
		public const string DisplayInformationControl = "DIF";

		/// <summary>
		///     Code for the 'Dimmer Control' command
		/// </summary>
		public const string DimmerControl = "DIM";

		/// <summary>
		///     Code for the 'Setup Operation Control' command
		/// </summary>
		public const string SetupOperationControl = "OSD";

		/// <summary>
		///     Code for the 'Memory Setup Control' command
		/// </summary>
		public const string MemorySetupControl = "MEM";

		/// <summary>
		///     Code for the 'Audio Information' command
		/// </summary>
		public const string AudioInformation = "IFA";

		/// <summary>
		///     Code for the 'Video Information' command
		/// </summary>
		public const string VideoInformation = "IFV";

		/// <summary>
		///     Code for the 'Input Selection' command
		/// </summary>
		public const string InputSelection = "SLI";

		/// <summary>
		///     Code for the 'Audio Selection' command
		/// </summary>
		public const string AudioSelection = "SLA";

		/// <summary>
		///     Code for the '12V Trigger A Control' command
		/// </summary>
		public const string TriggerAControl = "TGA";

		/// <summary>
		///     Code for the '12V Trigger B Control' command
		/// </summary>
		public const string TriggerBControl = "TGB";

		/// <summary>
		///     Code for the '12V Trigger C Control' command
		/// </summary>
		public const string TriggerCControl = "TGC";

		/// <summary>
		///     Code for the 'HDMI Out Selection' command
		/// </summary>
		public const string HDMIOutSelection = "HDO";

		/// <summary>
		///     Code for the 'HDMI Audio Out Selection (Main)' command
		/// </summary>
		public const string HDMIAudioOutSelectionMain = "HAO";

		/// <summary>
		///     Code for the 'HDMI Audio Out Selection (Sub)' command
		/// </summary>
		public const string HDMIAudioOutSelectionSub = "HAS";

		/// <summary>
		///     Code for the 'HDMI CEC Enabled' command
		/// </summary>
		public const string HDMICECEnabled = "CEC";

		/// <summary>
		///     Code for the 'HDMI CEC Control Monitor Type' command
		/// </summary>
		public const string HDMICECControlMonitorType = "CCM";

		/// <summary>
		///     Code for the 'Output Resolution' command
		/// </summary>
		public const string OutputResolution = "RES";

		/// <summary>
		///     Code for the 'Video Wide Mode' command
		/// </summary>
		public const string VideoWideMode = "VWM";

		/// <summary>
		///     Code for the 'Video Picture Mode' command
		/// </summary>
		public const string VideoPictureMode = "VPM";

		/// <summary>
		///     Code for the 'Listening Mode' command
		/// </summary>
		public const string ListeningMode = "LMD";

		/// <summary>
		///     Code for the 'Late Night Setting' command
		/// </summary>
		public const string LateNightSetting = "LTN";

		/// <summary>
		///     Code for the 'Re-EQ/Cinema Filter Enabled' command
		/// </summary>
		public const string ReEQCinemaFilterEnabled = "RAS";

		/// <summary>
		///     Code for the 'Audyssey MultEQ Enabled' command
		/// </summary>
		public const string AudysseyMultEQEnabled = "ADY";

		/// <summary>
		///     Code for the 'Audyssey Dynamic EQ Enabled' command
		/// </summary>
		public const string AudysseyDynamicEQEnabled = "ADQ";

		/// <summary>
		///     Code for the 'Audyssey Dynamic Volume Enabled' command
		/// </summary>
		public const string AudysseyDynamicVolumeEnabled = "ADV";

		/// <summary>
		///     Code for the 'Dolby Volume Level' command
		/// </summary>
		public const string DolbyVolumeLevel = "DVL";

		/// <summary>
		///     Code for the 'AccuEQ Enabled' command
		/// </summary>
		public const string AccuEQEnabled = "AEQ";

		/// <summary>
		///     Code for the 'Music Optimizer Enabled' command
		/// </summary>
		public const string MusicOptimizerEnabled = "MOT";

		/// <summary>
		///     Code for the 'A/V Sync Configuration' command
		/// </summary>
		public const string AVSyncConfiguration = "AVS";

		/// <summary>
		///     Code for the 'Smart Grid Eco Configuration' command
		/// </summary>
		public const string SmartGridEcoConfiguration = "ECO";

		/// <summary>
		///     Code for the 'Update' command
		/// </summary>
		public const string Update = "UPD";

		/// <summary>
		///     Code for the 'Popup Message' command
		/// </summary>
		public const string PopupMessage = "POP";

		/// <summary>
		///     Code for the 'Tuning' command
		/// </summary>
		public const string Tuning = "TUN";

		/// <summary>
		///     Code for the 'Tuning Present' command
		/// </summary>
		public const string TuningPresent = "PRS";

		/// <summary>
		///     Code for the 'Tuning Preset Memory Control' command
		/// </summary>
		public const string TuningPresetMemoryControl = "PRM";

		/// <summary>
		///     Code for the 'RDS Information' command
		/// </summary>
		public const string RDSInformation = "RDS";

		/// <summary>
		///     Code for the 'PTY Scan' command
		/// </summary>
		public const string PTYScan = "PTY";

		/// <summary>
		///     Code for the 'TP Scan' command
		/// </summary>
		public const string TPScan = "TPS";

		/// <summary>
		///     Code for the 'HD Radio Artist Name' command
		/// </summary>
		public const string HDRadioArtistName = "HAT";

		/// <summary>
		///     Code for the 'HD Radio Channel Name' command
		/// </summary>
		public const string HDRadioChannelName = "HCN";

		/// <summary>
		///     Code for the 'HD Radio Title' command
		/// </summary>
		public const string HDRadioTitle = "HTI";

		/// <summary>
		///     Code for the 'HD Radio Detail' command
		/// </summary>
		public const string HDRadioDetail = "HDI";

		/// <summary>
		///     Code for the 'HD Radio Channel Program' command
		/// </summary>
		public const string HDRadioChannelProgram = "HPR";

		/// <summary>
		///     Code for the 'HD Radio Blend Mode' command
		/// </summary>
		public const string HDRadioBlendMode = "HBL";

		/// <summary>
		///     Code for the 'HD Radio Tuner State' command
		/// </summary>
		public const string HDRadioTunerState = "HTS";

		/// <summary>
		///     Code for the 'Network Remote Control' command
		/// </summary>
		public const string NetworkRemoteControl = "NTC";

		/// <summary>
		///     Code for the 'Bluetooth Control' command
		/// </summary>
		public const string BluetoothControl = "NBT";

		/// <summary>
		///     Code for the 'Network Artist Name' command
		/// </summary>
		public const string NetworkArtistName = "NAT";

		/// <summary>
		///     Code for the 'Network Album Name' command
		/// </summary>
		public const string NetworkAlbumName = "NAL";

		/// <summary>
		///     Code for the 'Network Title' command
		/// </summary>
		public const string NetworkTitle = "NTI";

		/// <summary>
		///     Code for the 'Network Time Info' command
		/// </summary>
		public const string NetworkTimeInfo = "NTM";

		/// <summary>
		///     Code for the 'Network Track Info' command
		/// </summary>
		public const string NetworkTrackInfo = "NTR";

		/// <summary>
		///     Code for the 'Network Play Status' command
		/// </summary>
		public const string NetworkPlayStatus = "NST";

		/// <summary>
		///     Code for the 'Network Menu Status' command
		/// </summary>
		public const string NetworkMenuStatus = "NMS";

		/// <summary>
		///     Code for the 'Network Time Seek' command
		/// </summary>
		public const string NetworkTimeSeek = "NTS";

		/// <summary>
		///     Code for the 'Internet Radio Preset' command
		/// </summary>
		public const string InternetRadioPreset = "NPR";

		/// <summary>
		///     Code for the 'Network Connection Status' command
		/// </summary>
		public const string NetworkConnectionStatus = "NDS";

		/// <summary>
		///     Code for the 'Network List Info' command
		/// </summary>
		public const string NetworkListInfo = "NLS";

		/// <summary>
		///     Code for the 'Network List Info (All)' command
		/// </summary>
		public const string NetworkListInfoAll = "NLA";

		/// <summary>
		///     Code for the 'Network Jacket Art' command
		/// </summary>
		public const string NetworkJacketArt = "NJA";

		/// <summary>
		///     Code for the 'Network Service Selection' command
		/// </summary>
		public const string NetworkServiceSelection = "NSV";

		/// <summary>
		///     Code for the 'Network Keyboard' command
		/// </summary>
		public const string NetworkKeyboard = "NKY";

		/// <summary>
		///     Code for the 'Network Popup' command
		/// </summary>
		public const string NetworkPopup = "NPU";

		/// <summary>
		///     Code for the 'Network List Title Info' command
		/// </summary>
		public const string NetworkListTitleInfo = "NLT";

		/// <summary>
		///     Code for the 'iPod Mode Change' command
		/// </summary>
		public const string IPodModeChange = "NMD";

		/// <summary>
		///     Code for the 'Network Standby' command
		/// </summary>
		public const string NetworkStandby = "NSB";

		/// <summary>
		///     Code for the 'Receiver Network Information' command
		/// </summary>
		public const string ReceiverNetworkInformation = "NRI";

		/// <summary>
		///     Code for the 'Network Custom Popup Message' command
		/// </summary>
		public const string NetworkCustomPopupMessage = "NCP";

		/// <summary>
		///     Code for the 'Network List Update' command
		/// </summary>
		public const string NetworkListUpdate = "NLU";

		/// <summary>
		///     Code for the 'Network Playback Status' command
		/// </summary>
		public const string NetworkPlaybackStatus = "NPB";

		/// <summary>
		///     Code for the 'Airplay Artist Name' command
		/// </summary>
		public const string AirplayArtistName = "AAT";

		/// <summary>
		///     Code for the 'Airplay Album Name' command
		/// </summary>
		public const string AirplayAlbumName = "AAL";

		/// <summary>
		///     Code for the 'Airplay Title Name' command
		/// </summary>
		public const string AirplayTitleName = "ATI";

		/// <summary>
		///     Code for the 'Airplay Time Info' command
		/// </summary>
		public const string AirplayTimeInfo = "ATM";

		/// <summary>
		///     Code for the 'Airplay Play Status' command
		/// </summary>
		public const string AirplayPlayStatus = "AST";

		public static readonly Dictionary<string, CommandId> Commands =
			new Dictionary<string, CommandId>() {
				                                    { "PWR", new CommandId("PWR", "Master Power") },
				                                    { "AMT", new CommandId("AMT", "Audio Muting") },
				                                    { "SPA", new CommandId("SPA", "Speaker A Control") },
				                                    { "SPB", new CommandId("SPB", "Speaker B Control") },
				                                    { "SPL", new CommandId("SPL", "Speaker Layout") },
				                                    { "MVL", new CommandId("MVL", "Master Volume") },
				                                    { "TFR", new CommandId("TFR", "Tone Front") },
				                                    { "TFW", new CommandId("TFW", "Tone Front Wide") },
				                                    { "TFH", new CommandId("TFH", "Tone Front High") },
				                                    { "TCT", new CommandId("TCT", "Tone Center") },
				                                    { "TSR", new CommandId("TSR", "Tone Surround") },
				                                    { "TSB", new CommandId("TSB", "Tone Surround Back") },
				                                    { "TSW", new CommandId("TSW", "Tone Subwoofer") },
				                                    { "PMB", new CommandId("PMB", "Phase-Matching Bass Control") },
				                                    { "SLP", new CommandId("SLP", "Sleep Control") },
				                                    { "SLC", new CommandId("SLC", "Speaker Level Calibration") },
				                                    { "SWL", new CommandId("SWL", "Subwoofer Level Control") },
				                                    { "SW2", new CommandId("SW2", "Subwoofer 2 Level Control") },
				                                    { "CTL", new CommandId("CTL", "Center Level Control") },
				                                    { "DIF", new CommandId("DIF", "Display Information Control") },
				                                    { "DIM", new CommandId("DIM", "Dimmer Control") },
				                                    { "OSD", new CommandId("OSD", "Setup Operation Control") },
				                                    { "MEM", new CommandId("MEM", "Memory Setup Control") },
				                                    { "IFA", new CommandId("IFA", "Audio Information") },
				                                    { "IFV", new CommandId("IFV", "Video Information") },
				                                    { "SLI", new CommandId("SLI", "Input Selection") },
				                                    { "SLA", new CommandId("SLA", "Audio Selection") },
				                                    { "TGA", new CommandId("TGA", "12V Trigger A Control") },
				                                    { "TGB", new CommandId("TGB", "12V Trigger B Control") },
				                                    { "TGC", new CommandId("TGC", "12V Trigger C Control") },
				                                    { "HDO", new CommandId("HDO", "HDMI Out Selection") },
				                                    { "HAO", new CommandId("HAO", "HDMI Audio Out Selection (Main)") },
				                                    { "HAS", new CommandId("HAS", "HDMI Audio Out Selection (Sub)") },
				                                    { "CEC", new CommandId("CEC", "HDMI CEC Enabled") },
				                                    { "CCM", new CommandId("CCM", "HDMI CEC Control Monitor Type") },
				                                    { "RES", new CommandId("RES", "Output Resolution") },
				                                    { "VWM", new CommandId("VWM", "Video Wide Mode") },
				                                    { "VPM", new CommandId("VPM", "Video Picture Mode") },
				                                    { "LMD", new CommandId("LMD", "Listening Mode") },
				                                    { "LTN", new CommandId("LTN", "Late Night Setting") },
				                                    { "RAS", new CommandId("RAS", "Re-EQ/Cinema Filter Enabled") },
				                                    { "ADY", new CommandId("ADY", "Audyssey MultEQ Enabled") },
				                                    { "ADQ", new CommandId("ADQ", "Audyssey Dynamic EQ Enabled") },
				                                    { "ADV", new CommandId("ADV", "Audyssey Dynamic Volume Enabled") },
				                                    { "DVL", new CommandId("DVL", "Dolby Volume Level") },
				                                    { "AEQ", new CommandId("AEQ", "AccuEQ Enabled") },
				                                    { "MOT", new CommandId("MOT", "Music Optimizer Enabled") },
				                                    { "AVS", new CommandId("AVS", "A/V Sync Configuration") },
				                                    { "ECO", new CommandId("ECO", "Smart Grid Eco Configuration") },
				                                    { "UPD", new CommandId("UPD", "Update") },
				                                    { "POP", new CommandId("POP", "Popup Message") },
				                                    { "TUN", new CommandId("TUN", "Tuning") },
				                                    { "PRS", new CommandId("PRS", "Tuning Present") },
				                                    { "PRM", new CommandId("PRM", "Tuning Preset Memory Control") },
				                                    { "RDS", new CommandId("RDS", "RDS Information") },
				                                    { "PTY", new CommandId("PTY", "PTY Scan") },
				                                    { "TPS", new CommandId("TPS", "TP Scan") },
				                                    { "HAT", new CommandId("HAT", "HD Radio Artist Name") },
				                                    { "HCN", new CommandId("HCN", "HD Radio Channel Name") },
				                                    { "HTI", new CommandId("HTI", "HD Radio Title") },
				                                    { "HDI", new CommandId("HDI", "HD Radio Detail") },
				                                    { "HPR", new CommandId("HPR", "HD Radio Channel Program") },
				                                    { "HBL", new CommandId("HBL", "HD Radio Blend Mode") },
				                                    { "HTS", new CommandId("HTS", "HD Radio Tuner State") },
				                                    { "NTC", new CommandId("NTC", "Network Remote Control") },
				                                    { "NBT", new CommandId("NBT", "Bluetooth Control") },
				                                    { "NAT", new CommandId("NAT", "Network Artist Name") },
				                                    { "NAL", new CommandId("NAL", "Network Album Name") },
				                                    { "NTI", new CommandId("NTI", "Network Title") },
				                                    { "NTM", new CommandId("NTM", "Network Time Info") },
				                                    { "NTR", new CommandId("NTR", "Network Track Info") },
				                                    { "NST", new CommandId("NST", "Network Play Status") },
				                                    { "NMS", new CommandId("NMS", "Network Menu Status") },
				                                    { "NTS", new CommandId("NTS", "Network Time Seek") },
				                                    { "NPR", new CommandId("NPR", "Internet Radio Preset") },
				                                    { "NDS", new CommandId("NDS", "Network Connection Status") },
				                                    { "NLS", new CommandId("NLS", "Network List Info") },
				                                    { "NLA", new CommandId("NLA", "Network List Info (All)") },
				                                    { "NJA", new CommandId("NJA", "Network Jacket Art") },
				                                    { "NSV", new CommandId("NSV", "Network Service Selection") },
				                                    { "NKY", new CommandId("NKY", "Network Keyboard") },
				                                    { "NPU", new CommandId("NPU", "Network Popup") },
				                                    { "NLT", new CommandId("NLT", "Network List Title Info") },
				                                    { "NMD", new CommandId("NMD", "iPod Mode Change") },
				                                    { "NSB", new CommandId("NSB", "Network Standby") },
				                                    { "NRI", new CommandId("NRI", "Receiver Network Information") },
				                                    { "NCP", new CommandId("NCP", "Network Custom Popup Message") },
				                                    { "NLU", new CommandId("NLU", "Network List Update") },
				                                    { "NPB", new CommandId("NPB", "Network Playback Status") },
				                                    { "AAT", new CommandId("AAT", "Airplay Artist Name") },
				                                    { "AAL", new CommandId("AAL", "Airplay Album Name") },
				                                    { "ATI", new CommandId("ATI", "Airplay Title Name") },
				                                    { "ATM", new CommandId("ATM", "Airplay Time Info") },
				                                    { "AST", new CommandId("AST", "Airplay Play Status") },
			                                    };
		private CommandId(string code, string friendlyName) {
			this.Code = code;
			this.FriendlyName = friendlyName;
		}

		public string Code { get; set; }
		public string FriendlyName { get; set; }
	}
}
