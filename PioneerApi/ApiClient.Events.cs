namespace PioneerApi {
	using System;
	using System.Collections.Generic;
	using System.Text;

	public partial class ApiClient {
		/// <summary>
		///     Event handler called when the 'Master Power' command is received (PWR)
		/// </summary>
		public event EventHandler<bool> OnMasterPower;

		/// <summary>
		///     Event handler called when the 'Audio Muting' command is received (AMT)
		/// </summary>
		public event EventHandler<string> OnAudioMuting;

		/// <summary>
		///     Event handler called when the 'Speaker A Control' command is received (SPA)
		/// </summary>
		public event EventHandler<string> OnSpeakerAControl;

		/// <summary>
		///     Event handler called when the 'Speaker B Control' command is received (SPB)
		/// </summary>
		public event EventHandler<string> OnSpeakerBControl;

		/// <summary>
		///     Event handler called when the 'Speaker Layout' command is received (SPL)
		/// </summary>
		public event EventHandler<string> OnSpeakerLayout;

		/// <summary>
		///     Event handler called when the 'Master Volume' command is received (MVL)
		/// </summary>
		public event EventHandler<int> OnMasterVolume;

		/// <summary>
		///     Event handler called when the 'Tone Front' command is received (TFR)
		/// </summary>
		public event EventHandler<string> OnToneFront;

		/// <summary>
		///     Event handler called when the 'Tone Front Wide' command is received (TFW)
		/// </summary>
		public event EventHandler<string> OnToneFrontWide;

		/// <summary>
		///     Event handler called when the 'Tone Front High' command is received (TFH)
		/// </summary>
		public event EventHandler<string> OnToneFrontHigh;

		/// <summary>
		///     Event handler called when the 'Tone Center' command is received (TCT)
		/// </summary>
		public event EventHandler<string> OnToneCenter;

		/// <summary>
		///     Event handler called when the 'Tone Surround' command is received (TSR)
		/// </summary>
		public event EventHandler<string> OnToneSurround;

		/// <summary>
		///     Event handler called when the 'Tone Surround Back' command is received (TSB)
		/// </summary>
		public event EventHandler<string> OnToneSurroundBack;

		/// <summary>
		///     Event handler called when the 'Tone Subwoofer' command is received (TSW)
		/// </summary>
		public event EventHandler<string> OnToneSubwoofer;

		/// <summary>
		///     Event handler called when the 'Phase-Matching Bass Control' command is received (PMB)
		/// </summary>
		public event EventHandler<string> OnPhaseMatchingBassControl;

		/// <summary>
		///     Event handler called when the 'Sleep Control' command is received (SLP)
		/// </summary>
		public event EventHandler<string> OnSleepControl;

		/// <summary>
		///     Event handler called when the 'Speaker Level Calibration' command is received (SLC)
		/// </summary>
		public event EventHandler<string> OnSpeakerLevelCalibration;

		/// <summary>
		///     Event handler called when the 'Subwoofer Level Control' command is received (SWL)
		/// </summary>
		public event EventHandler<string> OnSubwooferLevelControl;

		/// <summary>
		///     Event handler called when the 'Subwoofer 2 Level Control' command is received (SW2)
		/// </summary>
		public event EventHandler<string> OnSubwoofer2LevelControl;

		/// <summary>
		///     Event handler called when the 'Center Level Control' command is received (CTL)
		/// </summary>
		public event EventHandler<string> OnCenterLevelControl;

		/// <summary>
		///     Event handler called when the 'Display Information Control' command is received (DIF)
		/// </summary>
		public event EventHandler<string> OnDisplayInformationControl;

		/// <summary>
		///     Event handler called when the 'Dimmer Control' command is received (DIM)
		/// </summary>
		public event EventHandler<string> OnDimmerControl;

		/// <summary>
		///     Event handler called when the 'Setup Operation Control' command is received (OSD)
		/// </summary>
		public event EventHandler<string> OnSetupOperationControl;

		/// <summary>
		///     Event handler called when the 'Memory Setup Control' command is received (MEM)
		/// </summary>
		public event EventHandler<string> OnMemorySetupControl;

		/// <summary>
		///     Event handler called when the 'Audio Information' command is received (IFA)
		/// </summary>
		public event EventHandler<string> OnAudioInformation;

		/// <summary>
		///     Event handler called when the 'Video Information' command is received (IFV)
		/// </summary>
		public event EventHandler<string> OnVideoInformation;

		/// <summary>
		///     Event handler called when the 'Input Selection' command is received (SLI)
		/// </summary>
		public event EventHandler<string> OnInputSelection;

		/// <summary>
		///     Event handler called when the 'Audio Selection' command is received (SLA)
		/// </summary>
		public event EventHandler<string> OnAudioSelection;

		/// <summary>
		///     Event handler called when the '12V Trigger A Control' command is received (TGA)
		/// </summary>
		public event EventHandler<string> OnTriggerAControl;

		/// <summary>
		///     Event handler called when the '12V Trigger B Control' command is received (TGB)
		/// </summary>
		public event EventHandler<string> OnTriggerBControl;

		/// <summary>
		///     Event handler called when the '12V Trigger C Control' command is received (TGC)
		/// </summary>
		public event EventHandler<string> OnTriggerCControl;

		/// <summary>
		///     Event handler called when the 'HDMI Out Selection' command is received (HDO)
		/// </summary>
		public event EventHandler<string> OnHDMIOutSelection;

		/// <summary>
		///     Event handler called when the 'HDMI Audio Out Selection (Main)' command is received (HAO)
		/// </summary>
		public event EventHandler<string> OnHDMIAudioOutSelectionMain;

		/// <summary>
		///     Event handler called when the 'HDMI Audio Out Selection (Sub)' command is received (HAS)
		/// </summary>
		public event EventHandler<string> OnHDMIAudioOutSelectionSub;

		/// <summary>
		///     Event handler called when the 'HDMI CEC Enabled' command is received (CEC)
		/// </summary>
		public event EventHandler<string> OnHDMICECEnabled;

		/// <summary>
		///     Event handler called when the 'HDMI CEC Control Monitor Type' command is received (CCM)
		/// </summary>
		public event EventHandler<string> OnHDMICECControlMonitorType;

		/// <summary>
		///     Event handler called when the 'Output Resolution' command is received (RES)
		/// </summary>
		public event EventHandler<string> OnOutputResolution;

		/// <summary>
		///     Event handler called when the 'Video Wide Mode' command is received (VWM)
		/// </summary>
		public event EventHandler<string> OnVideoWideMode;

		/// <summary>
		///     Event handler called when the 'Video Picture Mode' command is received (VPM)
		/// </summary>
		public event EventHandler<string> OnVideoPictureMode;

		/// <summary>
		///     Event handler called when the 'Listening Mode' command is received (LMD)
		/// </summary>
		public event EventHandler<string> OnListeningMode;

		/// <summary>
		///     Event handler called when the 'Late Night Setting' command is received (LTN)
		/// </summary>
		public event EventHandler<string> OnLateNightSetting;

		/// <summary>
		///     Event handler called when the 'Re-EQ/Cinema Filter Enabled' command is received (RAS)
		/// </summary>
		public event EventHandler<string> OnReEQCinemaFilterEnabled;

		/// <summary>
		///     Event handler called when the 'Audyssey MultEQ Enabled' command is received (ADY)
		/// </summary>
		public event EventHandler<string> OnAudysseyMultEQEnabled;

		/// <summary>
		///     Event handler called when the 'Audyssey Dynamic EQ Enabled' command is received (ADQ)
		/// </summary>
		public event EventHandler<string> OnAudysseyDynamicEQEnabled;

		/// <summary>
		///     Event handler called when the 'Audyssey Dynamic Volume Enabled' command is received (ADV)
		/// </summary>
		public event EventHandler<string> OnAudysseyDynamicVolumeEnabled;

		/// <summary>
		///     Event handler called when the 'Dolby Volume Level' command is received (DVL)
		/// </summary>
		public event EventHandler<string> OnDolbyVolumeLevel;

		/// <summary>
		///     Event handler called when the 'AccuEQ Enabled' command is received (AEQ)
		/// </summary>
		public event EventHandler<string> OnAccuEQEnabled;

		/// <summary>
		///     Event handler called when the 'Music Optimizer Enabled' command is received (MOT)
		/// </summary>
		public event EventHandler<string> OnMusicOptimizerEnabled;

		/// <summary>
		///     Event handler called when the 'A/V Sync Configuration' command is received (AVS)
		/// </summary>
		public event EventHandler<string> OnAVSyncConfiguration;

		/// <summary>
		///     Event handler called when the 'Smart Grid Eco Configuration' command is received (ECO)
		/// </summary>
		public event EventHandler<string> OnSmartGridEcoConfiguration;

		/// <summary>
		///     Event handler called when the 'Update' command is received (UPD)
		/// </summary>
		public event EventHandler<string> OnUpdate;

		/// <summary>
		///     Event handler called when the 'Popup Message' command is received (POP)
		/// </summary>
		public event EventHandler<string> OnPopupMessage;

		/// <summary>
		///     Event handler called when the 'Tuning' command is received (TUN)
		/// </summary>
		public event EventHandler<string> OnTuning;

		/// <summary>
		///     Event handler called when the 'Tuning Present' command is received (PRS)
		/// </summary>
		public event EventHandler<string> OnTuningPresent;

		/// <summary>
		///     Event handler called when the 'Tuning Preset Memory Control' command is received (PRM)
		/// </summary>
		public event EventHandler<string> OnTuningPresetMemoryControl;

		/// <summary>
		///     Event handler called when the 'RDS Information' command is received (RDS)
		/// </summary>
		public event EventHandler<string> OnRDSInformation;

		/// <summary>
		///     Event handler called when the 'PTY Scan' command is received (PTY)
		/// </summary>
		public event EventHandler<string> OnPTYScan;

		/// <summary>
		///     Event handler called when the 'TP Scan' command is received (TPS)
		/// </summary>
		public event EventHandler<string> OnTPScan;

		/// <summary>
		///     Event handler called when the 'HD Radio Artist Name' command is received (HAT)
		/// </summary>
		public event EventHandler<string> OnHDRadioArtistName;

		/// <summary>
		///     Event handler called when the 'HD Radio Channel Name' command is received (HCN)
		/// </summary>
		public event EventHandler<string> OnHDRadioChannelName;

		/// <summary>
		///     Event handler called when the 'HD Radio Title' command is received (HTI)
		/// </summary>
		public event EventHandler<string> OnHDRadioTitle;

		/// <summary>
		///     Event handler called when the 'HD Radio Detail' command is received (HDI)
		/// </summary>
		public event EventHandler<string> OnHDRadioDetail;

		/// <summary>
		///     Event handler called when the 'HD Radio Channel Program' command is received (HPR)
		/// </summary>
		public event EventHandler<string> OnHDRadioChannelProgram;

		/// <summary>
		///     Event handler called when the 'HD Radio Blend Mode' command is received (HBL)
		/// </summary>
		public event EventHandler<string> OnHDRadioBlendMode;

		/// <summary>
		///     Event handler called when the 'HD Radio Tuner State' command is received (HTS)
		/// </summary>
		public event EventHandler<string> OnHDRadioTunerState;

		/// <summary>
		///     Event handler called when the 'Network Remote Control' command is received (NTC)
		/// </summary>
		public event EventHandler<string> OnNetworkRemoteControl;

		/// <summary>
		///     Event handler called when the 'Bluetooth Control' command is received (NBT)
		/// </summary>
		public event EventHandler<string> OnBluetoothControl;

		/// <summary>
		///     Event handler called when the 'Network Artist Name' command is received (NAT)
		/// </summary>
		public event EventHandler<string> OnNetworkArtistName;

		/// <summary>
		///     Event handler called when the 'Network Album Name' command is received (NAL)
		/// </summary>
		public event EventHandler<string> OnNetworkAlbumName;

		/// <summary>
		///     Event handler called when the 'Network Title' command is received (NTI)
		/// </summary>
		public event EventHandler<string> OnNetworkTitle;

		/// <summary>
		///     Event handler called when the 'Network Track Info' command is received (NTR)
		/// </summary>
		public event EventHandler<string> OnNetworkTrackInfo;

		/// <summary>
		///     Event handler called when the 'Network Play Status' command is received (NST)
		/// </summary>
		public event EventHandler<PlayInfo> OnNetworkPlayStatus;

		/// <summary>
		///     Event handler called when the 'Network Menu Status' command is received (NMS)
		/// </summary>
		public event EventHandler<string> OnNetworkMenuStatus;

		/// <summary>
		///     Event handler called when the 'Network Time Seek' command is received (NTS)
		/// </summary>
		public event EventHandler<string> OnNetworkTimeSeek;

		/// <summary>
		///     Event handler called when the 'Internet Radio Preset' command is received (NPR)
		/// </summary>
		public event EventHandler<string> OnInternetRadioPreset;

		/// <summary>
		///     Event handler called when the 'Network Connection Status' command is received (NDS)
		/// </summary>
		public event EventHandler<string> OnNetworkConnectionStatus;

		/// <summary>
		///     Event handler called when the 'Network List Info' command is received (NLS)
		/// </summary>
		public event EventHandler<string> OnNetworkListInfo;

		/// <summary>
		///     Event handler called when the 'Network List Info (All)' command is received (NLA)
		/// </summary>
		public event EventHandler<ListItemsResponse> OnNetworkListInfoAll;

		/// <summary>
		///     Event handler called when the 'Network Jacket Art' command is received (NJA)
		/// </summary>
		public event EventHandler<string> OnNetworkJacketArt;

		/// <summary>
		///     Event handler called when the 'Network Service Selection' command is received (NSV)
		/// </summary>
		public event EventHandler<string> OnNetworkServiceSelection;

		/// <summary>
		///     Event handler called when the 'Network Keyboard' command is received (NKY)
		/// </summary>
		public event EventHandler<string> OnNetworkKeyboard;

		/// <summary>
		///     Event handler called when the 'Network Popup' command is received (NPU)
		/// </summary>
		public event EventHandler<string> OnNetworkPopup;

		/// <summary>
		///     Event handler called when the 'Network List Title Info' command is received (NLT)
		/// </summary>
		public event EventHandler<NetworkListTitleInfo> OnNetworkListTitleInfo;

		/// <summary>
		///     Event handler called when the 'iPod Mode Change' command is received (NMD)
		/// </summary>
		public event EventHandler<string> OnIPodModeChange;

		/// <summary>
		///     Event handler called when the 'Network Standby' command is received (NSB)
		/// </summary>
		public event EventHandler<string> OnNetworkStandby;

		/// <summary>
		///     Event handler called when the 'Receiver Network Information' command is received (NRI)
		/// </summary>
		public event EventHandler<ReceiverInformationResponse> OnReceiverNetworkInformation;

		/// <summary>
		///     Event handler called when the 'Network Custom Popup Message' command is received (NCP)
		/// </summary>
		public event EventHandler<string> OnNetworkCustomPopupMessage;

		/// <summary>
		///     Event handler called when the 'Network List Update' command is received (NLU)
		/// </summary>
		public event EventHandler<string> OnNetworkListUpdate;

		/// <summary>
		///     Event handler called when the 'Network Playback Status' command is received (NPB)
		/// </summary>
		public event EventHandler<string> OnNetworkPlaybackStatus;

		/// <summary>
		///     Event handler called when the 'Airplay Artist Name' command is received (AAT)
		/// </summary>
		public event EventHandler<string> OnAirplayArtistName;

		/// <summary>
		///     Event handler called when the 'Airplay Album Name' command is received (AAL)
		/// </summary>
		public event EventHandler<string> OnAirplayAlbumName;

		/// <summary>
		///     Event handler called when the 'Airplay Title Name' command is received (ATI)
		/// </summary>
		public event EventHandler<string> OnAirplayTitleName;

		/// <summary>
		///     Event handler called when the 'Airplay Time Info' command is received (ATM)
		/// </summary>
		public event EventHandler<string> OnAirplayTimeInfo;

		/// <summary>
		///     Event handler called when the 'Airplay Play Status' command is received (AST)
		/// </summary>
		public event EventHandler<string> OnAirplayPlayStatus;

		/// <summary>
		///     An event handler called when network time info is received (NTM)
		/// </summary>
		public event EventHandler<(TimeSpan?, TimeSpan?)> OnNetworkTimeInfo;
	}
}
