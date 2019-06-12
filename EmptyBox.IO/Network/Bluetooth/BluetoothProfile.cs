using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public enum BluetoothProfile : ushort
    {
        ServiceDiscoveryServerServiceClassID = 0x1000,
        BrowseGroupDescriptorServiceClassID = 0x1001,
        SerialPort = 0x1101,
        PublicBrowseRoot = 0x1002,
        [Obsolete]
        LANAccessUsingPPP = 0x1102,
        DialupNetworking = 0x1103,
        IrMCSync = 0x1104,
        OBEXObjectPush = 0x1105,
        OBEXFileTransfer = 0x1106,
        IrMCSyncCommand = 0x1107,
        Headset = 0x1108,
        [Obsolete]
        CordlessTelephony = 0x1109,
        AudioSource = 0x110A,
        AudioSink = 0x110B,
        AV_RemoteControlTarget = 0x110C,
        AdvancedAudioDistribution = 0x110D,
        AV_RemoteControl = 0x110E,
        AV_RemoteControlController = 0x110F,
        [Obsolete]
        Intercom = 0x1110,
        [Obsolete]
        Fax = 0x1111,
        Headset_AudioGateway = 0x1112,
        [Obsolete]
        WAP = 0x1113,
        [Obsolete]
        WAP_Client = 0x1114,
        PANU = 0x1115,
        NAP = 0x1116,
        GN = 0x1117,
        DirectPrinting = 0x1118,
        ReferencePrinting = 0x1119,
        BasicImaginingProfile = 0x111A,
        ImagingResponder = 0x111B,
        ImagingAutomaticArchive = 0x111C,
        ImagingReferenceObjects = 0x111D,
        Handsfree = 0x111E,
        HandsfreeAudioGateway = 0x111F,
        DirectPrintingReferenceObjectsService = 0x1120,
        ReflectedUI = 0x1121,
        BasicPrinting = 0x1122,
        PrintingStatus = 0x1123,
        HumanInterfaceDeviceService = 0x1124,
        HardcopyCableReplacement = 0x1125,
        HCR_Print = 0x1126,
        HCR_Scan = 0x1127,
        [Obsolete]
        Common_ISDN_Access = 0x1128,
        SIM_Access = 0x112D,
        PhonebookAccess_PCE = 0x112E,
        PhonebookAccess_PSE = 0x112F,
        PhonebookAccess = 0x1130,
        Headset_HS = 0x1131,
        MessageAccessServer = 0x1132,
        MessageNotificationServer = 0x1133,
        MessageAccessProfile = 0x1134,
        GNSS = 0x1135,
        GNSS_Server = 0x1136,
        _3DDisplay = 0x1137,
        _3DGlasses = 0x1138,
        _3DSynchronization = 0x1139,
        MPS_Profile = 0x113A,
        MPS_SC = 0x113B,
        CTN_AccessService = 0x113C,
        CTN_NotificationService = 0x113D,
        CTN_Profile = 0x113E,
        PnPInformation = 0x1200,
        GenericNetworking = 0x1201,
        GenericFileTransfer = 0x1202,
        GenericAudio = 0x1203,
        GenericTelephony = 0x1204,
        [Obsolete]
        UPNP_Service = 0x1205,
        [Obsolete]
        UPNP_IP_Service = 0x1206,
        [Obsolete]
        ESDP_UPNP_IP_PAN = 0x1300,
        [Obsolete]
        ESDP_UPNP_IP_LAP = 0x1301,
        [Obsolete]
        ESDP_UPNP_L2CAP = 0x1302,
        VideoSource = 0x1303,
        VideoSink = 0x1304,
        VideoDestribution = 0x1305,
        HDP = 0x1400,
        HDP_Source = 0x1401,
        HDP_Sink = 0x1402
    }
}
