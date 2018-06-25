using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices
{
    public enum IANAInterfaceType : ushort
    {
        /// <summary>
        /// None of the following.
        /// </summary>
        Other = 1,
        Regular1822 = 2,
        hdh1822 = 3,
        ddnX25 = 4,
        RFC877x25 = 5,
        /// <summary>
        /// For all Ethernet-like interfaces, regardless of speed, as per RFC3635.
        /// </summary>
        EthernetCsmacd = 6,
        /// <summary>
        /// Obsoleted via RFC3635. EthernetCsmacd(6) should be used instead.
        /// </summary>
        [Obsolete]
        ISO88023Csmacd = 7,
        ISO88024TokenBus = 8,
        ISO88025TokenRing = 9,
        ISO88026Man = 10,
        /// <summary>
        /// Obsoleted via RFC3635. EthernetCsmacd(6) should be used instead.
        /// </summary>
        [Obsolete]
        starLan = 11,
        proteon10Mbit = 12,
        proteon80Mbit = 13,
        hyperchannel = 14,
        fddi = 15,
        lapb = 16,
        sdlc = 17,
        /// <summary>
        /// DS1-MIB
        /// </summary>
        ds1 = 18,
        /// <summary>
        /// Obsolete. See DS1-MIB
        /// </summary>
        [Obsolete]
        e1 = 19,
        /// <summary>
        /// No longer used. See also RFC2127.
        /// </summary>
        [Obsolete]
        BasicISDN = 20,
        /// <summary>
        /// No longer used. See also RFC2127.
        /// </summary>
        [Obsolete]
        primaryISDN = 21,
        /// <summary>
        /// Proprietary serial
        /// </summary>
        propPointToPointSerial = 22,
        ppp = 23,
        softwareLoopback = 24,
        /// <summary>
        /// CLNP over IP
        /// </summary>
        eon = 25,
        Ethernet3Mbit = 26,
        /// <summary>
        /// XNS over IP
        /// </summary>
        nsip = 27,
        /// <summary>
        /// generic SLIP
        /// </summary>
        slip = 28,
        /// <summary>
        /// ULTRA technologies
        /// </summary>
        ultra = 29,
        /// <summary>
        /// DS3-MIB
        /// </summary>
        ds3 = 30,
        /// <summary>
        /// SMDS, coffee
        /// </summary>
        sip = 31,
        /// <summary>
        /// DTE only.
        /// </summary>
        frameRelay = 32,
        rs232 = 33,
        /// <summary>
        /// Parallel-port
        /// </summary>
        para = 34,
        /// <summary>
        /// arcnet
        /// </summary>
        arcnet = 35,
        /// <summary>
        /// arcnet plus
        /// </summary>
        arcnetPlus = 36,
        /// <summary>
        /// ATM cells
        /// </summary>
        atm = 37,
        miox25 = 38,
        /// <summary>
        /// SONET or SDH
        /// </summary>
        sonet = 39,
        x25ple = 40,
        ISO88022llc = 41,
        localTalk = 42,
        smdsDxi = 43,
        /// <summary>
        /// FRNETSERV-MIB
        /// </summary>
        frameRelayService = 44,
        v35 = 45,
        hssi = 46,
        hippi = 47,
        /// <summary>
        /// Generic modem
        /// </summary>
        modem = 48,
        /// <summary>
        /// AAL5 over ATM
        /// </summary>
        aal5 = 49,
        sonetPath = 50,
        sonetVT = 51,
        /// <summary>
        /// SMDS InterCarrier Interface
        /// </summary>
        smdsIcip = 52,
        /// <summary>
        /// proprietary virtual/internal
        /// </summary>
        propVirtual = 53,
        /// <summary>
        /// proprietary multiplexing
        /// </summary>
        propMultiplexor = 54,
        /// <summary>
        /// 100BaseVG
        /// </summary>
        ieee80212 = 55,
        /// <summary>
        /// Fibre Channel
        /// </summary>
        fibreChannel = 56,
        /// <summary>
        /// HIPPI interfaces
        /// </summary>
        hippiInterface = 57,
        /// <summary>
        /// Obsolete, use either frameRelay(32) or frameRelayService(44).
        /// </summary>
        [Obsolete]
        frameRelayInterconnect = 58,
        /// <summary>
        /// ATM Emulated LAN for 802.3
        /// </summary>
        aflane8023 = 59,
        /// <summary>
        /// ATM Emulated LAN for 802.5
        /// </summary>
        aflane8025 = 60,
        /// <summary>
        /// ATM Emulated circuit
        /// </summary>
        cctEmul = 61,
        /// <summary>
        /// Obsoleted via RFC3635. EthernetCsmacd(6) should be used instead.
        /// </summary>
        [Obsolete]
        fastEther = 62,
        /// <summary>
        /// ISDN and X.25
        /// </summary>
        isdn = 63,
        /// <summary>
        /// CCITT V.11/X.21
        /// </summary>
        v11 = 64,
        /// <summary>
        /// CCITT V.36
        /// </summary>
        v36 = 65,
        /// <summary>
        /// CCITT G703 at 64Kbps
        /// </summary>
        g703at64k = 66,
        /// <summary>
        /// Obsolete. See DS1-MIB
        /// </summary>
        [Obsolete]
        g703at2mb = 67,
        /// <summary>
        /// SNA QLLC
        /// </summary>
        qllc = 68,
        /// <summary>
        /// Obsoleted via RFC3635. EthernetCsmacd(6) should be used instead.
        /// </summary>
        [Obsolete]
        fastEtherFX = 69,
        /// <summary>
        /// Channel
        /// </summary>
        channel = 70,
        /// <summary>
        /// radio spread spectrum
        /// </summary>
        ieee80211 = 71,
        /// <summary>
        /// IBM System 360/370 OEMI Channel
        /// </summary>
        ibm370parChan = 72,
        /// <summary>
        /// IBM Enterprise Systems Connection
        /// </summary>
        escon = 73,
        /// <summary>
        /// Data Link Switching
        /// </summary>
        dlsw = 74,
        /// <summary>
        /// ISDN S/T interface
        /// </summary>
        isdns = 75,
        /// <summary>
        /// ISDN U interface
        /// </summary>
        isdnu = 76,
        /// <summary>
        /// Link Access Protocol D
        /// </summary>
        lapd = 77,
        /// <summary>
        /// IP Switching Objects
        /// </summary>
        ipSwitch = 78,
        /// <summary>
        /// Remote Source Route Bridging
        /// </summary>
        rsrb = 79,
        /// <summary>
        /// ATM Logical Port
        /// </summary>
        atmLogical = 80,
        /// <summary>
        /// Digital Signal Level 0
        /// </summary>
        ds0 = 81,
        /// <summary>
        /// group of ds0s on the same ds1
        /// </summary>
        ds0Bundle = 82,
        /// <summary>
        /// Bisynchronous Protocol
        /// </summary>
        bsc = 83,
        /// <summary>
        /// Asynchronous Protocol
        /// </summary>
        async = 84,
        /// <summary>
        /// Combat Net Radio
        /// </summary>
        cnr = 85,
        /// <summary>
        /// ISO 802.5r DTR
        /// </summary>
        ISO88025Dtr = 86,
        /// <summary>
        /// Ext Pos Loc Report Sys
        /// </summary>
        eplrs = 87,
        /// <summary>
        /// Appletalk Remote Access Protocol
        /// </summary>
        arap = 88,
        /// <summary>
        /// Proprietary Connectionless Protocol
        /// </summary>
        propCnls = 89,
        /// <summary>
        /// CCITT-ITU X.29 PAD Protocol
        /// </summary>
        hostPad = 90,
        /// <summary>
        /// CCITT-ITU X.3 PAD Facility
        /// </summary>
        termPad = 91,
        /// <summary>
        /// Multiproto Interconnect over FR
        /// </summary>
        frameRelayMPI = 92,
        /// <summary>
        /// CCITT-ITU X213
        /// </summary>
        x213 = 93,
        /// <summary>
        /// Asymmetric Digital Subscriber Loop
        /// </summary>
        ADSL = 94,
        /// <summary>
        /// Rate-Adapt.Digital Subscriber Loop
        /// </summary>
        RADSL = 95,
        /// <summary>
        /// Symmetric Digital Subscriber Loop
        /// </summary>
        SDSL = 96,
        /// <summary>
        /// Very H-Speed Digital Subscrib.Loop
        /// </summary>
        VDSL = 97,
        /// <summary>
        /// ISO 802.5 CRFP
        /// </summary>
        ISO88025CRFPInt = 98,
        /// <summary>
        /// Myricom Myrinet
        /// </summary>
        myrinet = 99,
        /// <summary>
        /// voice recEive and transMit
        /// </summary>
        voiceEM = 100,
        /// <summary>
        /// voice Foreign Exchange Office
        /// </summary>
        voiceFXO = 101,
        /// <summary>
        /// voice Foreign Exchange Station
        /// </summary>
        voiceFXS = 102,
        /// <summary>
        /// voice encapsulation
        /// </summary>
        voiceEncap = 103,
        /// <summary>
        /// voice over IP encapsulation
        /// </summary>
        voiceOverIp = 104,
        /// <summary>
        /// ATM DXI
        /// </summary>
        atmDxi = 105,
        /// <summary>
        /// ATM FUNI
        /// </summary>
        atmFuni = 106,
        /// <summary>
        /// ATM IMA
        /// </summary>
        atmIma = 107,
        /// <summary>
        /// PPP Multilink Bundle
        /// </summary>
        pppMultilinkBundle = 108,
        /// <summary>
        /// IBM ipOverCdlc
        /// </summary>
        ipOverCdlc = 109,
        /// <summary>
        /// IBM Common Link Access to Workstn
        /// </summary>
        ipOverClaw = 110,
        /// <summary>
        /// IBM stackToStack
        /// </summary>
        stackToStack = 111,
        /// <summary>
        /// IBM VIPA
        /// </summary>
        virtualIpAddress = 112,
        /// <summary>
        /// IBM multi-protocol channel support
        /// </summary>
        mpc = 113,
        /// <summary>
        /// IBM ipOverAtm
        /// </summary>
        ipOverAtm = 114,
        /// <summary>
        /// ISO 802.5j Fiber Token Ring
        /// </summary>
        ISO88025Fiber = 115,
        /// <summary>
        /// IBM twinaxial data link control
        /// </summary>
        tdlc = 116,
        /// <summary>
        /// Obsoleted via RFC3635. EthernetCsmacd(6) should be used instead.
        /// </summary>
        [Obsolete]
        GigabitEthernet = 117,
        /// <summary>
        /// HDLC
        /// </summary>
        HDLC = 118,
        /// <summary>
        /// LAP F
        /// </summary>
        lapf = 119,
        /// <summary>
        /// V.37
        /// </summary>
        V37 = 120,
        /// <summary>
        /// Multi-Link Protocol
        /// </summary>
        x25mlp = 121,
        /// <summary>
        /// X25 Hunt Group
        /// </summary>
        x25huntGroup = 122,
        /// <summary>
        /// Transp HDLC
        /// </summary>
        TranspHDLC = 123,
        /// <summary>
        /// Interleave channel
        /// </summary>
        Interleave = 124,
        /// <summary>
        /// Fast channel
        /// </summary>
        Fast = 125,
        /// <summary>
        /// IP = for APPN HPR in IP networks
        /// </summary>
        IP = 126,
        /// <summary>
        /// CATV Mac Layer
        /// </summary>
        docsCableMaclayer = 127,
        /// <summary>
        /// CATV Downstream interface
        /// </summary>
        docsCableDownstream = 128,
        /// <summary>
        /// CATV Upstream interface
        /// </summary>
        docsCableUpstream = 129,
        /// <summary>
        /// Avalon Parallel Processor
        /// </summary>
        a12MppSwitch = 130,
        /// <summary>
        /// Encapsulation interface
        /// </summary>
        tunnel = 131,
        /// <summary>
        /// coffee pot
        /// </summary>
        coffee = 132,
        /// <summary>
        /// Circuit Emulation Service
        /// </summary>
        ces = 133,
        /// <summary>
        /// ATM Sub Interface
        /// </summary>
        atmSubInterface = 134,
        /// <summary>
        /// Layer 2 Virtual LAN using 802.1Q
        /// </summary>
        l2vlan = 135,
        /// <summary>
        /// Layer 3 Virtual LAN using IP
        /// </summary>
        l3ipvlan = 136,
        /// <summary>
        /// Layer 3 Virtual LAN using IPX
        /// </summary>
        l3ipxvlan = 137,
        /// <summary>
        /// IP over Power Lines
        /// </summary>
        digitalPowerline = 138,
        /// <summary>
        /// Multimedia Mail over IP
        /// </summary>
        mediaMailOverIp = 139,
        /// <summary>
        /// Dynamic syncronous Transfer Mode
        /// </summary>
        dtm = 140,
        /// <summary>
        /// Data Communications Network
        /// </summary>
        dcn = 141,
        /// <summary>
        /// IP Forwarding Interface
        /// </summary>
        ipForward = 142,
        /// <summary>
        /// Multi-rate Symmetric DSL
        /// </summary>
        MSDSL = 143,
        /// <summary>
        /// IEEE1394 High Performance Serial Bus
        /// </summary>
        ieee1394 = 144,
        /// <summary>
        /// HIPPI-6400
        /// </summary>
        if_gsn = 145,
        /// <summary>
        /// DVB-RCC MAC Layer
        /// </summary>
        dvbRccMacLayer = 146,
        /// <summary>
        /// DVB-RCC Downstream Channel
        /// </summary>
        dvbRccDownstream = 147,
        /// <summary>
        /// DVB-RCC Upstream Channel
        /// </summary>
        dvbRccUpstream = 148,
        /// <summary>
        /// ATM Virtual Interface
        /// </summary>
        atmVirtual = 149,
        /// <summary>
        /// MPLS Tunnel Virtual Interface
        /// </summary>
        mplsTunnel = 150,
        /// <summary>
        /// Spatial Reuse Protocol
        /// </summary>
        srp = 151,
        /// <summary>
        /// Voice Over ATM
        /// </summary>
        voiceOverAtm = 152,
        /// <summary>
        /// Voice Over Frame Relay
        /// </summary>
        voiceOverFrameRelay = 153,
        /// <summary>
        /// Digital Subscriber Loop over ISDN
        /// </summary>
        IDSL = 154,
        /// <summary>
        /// Avici Composite Link Interface
        /// </summary>
        compositeLink = 155,
        /// <summary>
        /// SS7 Signaling Link
        /// </summary>
        ss7SigLink = 156,
        /// <summary>
        /// Prop.P2P wireless interface
        /// </summary>
        propWirelessP2P = 157,
        /// <summary>
        /// Frame Forward Interface
        /// </summary>
        frForward = 158,
        /// <summary>
        /// Multiprotocol over ATM AAL5
        /// </summary>
        RFC1483 = 159,
        /// <summary>
        /// USB Interface
        /// </summary>
        usb = 160,
        /// <summary>
        /// IEEE 802.3ad Link Aggregate
        /// </summary>
        ieee8023adLag = 161,
        /// <summary>
        /// BGP Policy Accounting
        /// </summary>
        bgppolicyaccounting = 162,
        /// <summary>
        /// FRF .16 Multilink Frame Relay
        /// </summary>
        frf16MfrBundle = 163,
        /// <summary>
        /// H323 Gatekeeper
        /// </summary>
        h323Gatekeeper = 164,
        /// <summary>
        /// H323 Voice and Video Proxy
        /// </summary>
        h323Proxy = 165,
        /// <summary>
        /// MPLS
        /// </summary>
        mpls = 166,
        /// <summary>
        /// Multi-frequency signaling link
        /// </summary>
        mfSigLink = 167,
        /// <summary>
        /// High Bit-Rate DSL - 2nd generation
        /// </summary>
        HDSL2 = 168,
        /// <summary>
        /// Multirate HDSL2
        /// </summary>
        shdsl = 169,
        /// <summary>
        /// Facility Data Link 4Kbps on a DS1
        /// </summary>
        ds1FDL = 170,
        /// <summary>
        /// Packet over SONET/SDH Interface
        /// </summary>
        pos = 171,
        /// <summary>
        /// DVB-ASI Input
        /// </summary>
        dvbAsiIn = 172,
        /// <summary>
        /// DVB-ASI Output
        /// </summary>
        dvbAsiOut = 173,
        /// <summary>
        /// Power Line Communtications
        /// </summary>
        plc = 174,
        /// <summary>
        /// Non Facility Associated Signaling
        /// </summary>
        nfas = 175,
        /// <summary>
        /// TR008
        /// </summary>
        tr008 = 176,
        /// <summary>
        /// Remote Digital Terminal
        /// </summary>
        gr303RDT = 177,
        /// <summary>
        /// Integrated Digital Terminal
        /// </summary>
        gr303IDT = 178,
        /// <summary>
        /// ISUP
        /// </summary>
        isup = 179,
        /// <summary>
        /// Cisco proprietary Maclayer
        /// </summary>
        propDocsWirelessMaclayer = 180,
        /// <summary>
        /// Cisco proprietary Downstream
        /// </summary>
        propDocsWirelessDownstream = 181,
        /// <summary>
        /// Cisco proprietary Upstream
        /// </summary>
        propDocsWirelessUpstream = 182,
        /// <summary>
        /// HIPERLAN Type 2 Radio Interface
        /// </summary>
        hiperlan2 = 183,
        /// <summary>
        /// PropBroadbandWirelessAccesspt2multipt
        /// Use of this iftype for IEEE 802.16 WMAN
        /// interfaces as per IEEE Std 802.16f is
        /// deprecated and ifType 237 should be used instead.
        /// </summary>
        propBWAp2Mp = 184,
        /// <summary>
        /// SONET Overhead Channel
        /// </summary>
        sonetOverheadChannel = 185,
        /// <summary>
        /// Digital Wrapper
        /// </summary>
        digitalWrapperOverheadChannel = 186,
        /// <summary>
        /// ATM adaptation layer 2
        /// </summary>
        aal2 = 187,
        /// <summary>
        /// MAC layer over radio links
        /// </summary>
        radioMAC = 188,
        /// <summary>
        /// ATM over radio links
        /// </summary>
        atmRadio = 189,
        /// <summary>
        /// Inter Machine Trunks
        /// </summary>
        imt = 190,
        /// <summary>
        /// Multiple Virtual Lines DSL
        /// </summary>
        mvl = 191,
        /// <summary>
        /// Long Reach DSL
        /// </summary>
        ReachDSL = 192,
        /// <summary>
        /// Frame Relay DLCI End Point
        /// </summary>
        frDlciEndPt = 193,
        /// <summary>
        /// ATM VCI End Point
        /// </summary>
        atmVciEndPt = 194,
        /// <summary>
        /// Optical Channel
        /// </summary>
        opticalChannel = 195,
        /// <summary>
        /// Optical Transport
        /// </summary>
        OpticalTransport = 196,
        /// <summary>
        /// Proprietary ATM
        /// </summary>
        PropATM = 197,
        /// <summary>
        /// Voice Over Cable Interface
        /// </summary>
        VoiceOverCable = 198,
        /// <summary>
        /// Infiniband
        /// </summary>
        Infiniband = 199,
        /// <summary>
        /// TE Link
        /// </summary>
        TELink = 200,
        /// <summary>
        /// Q.2931
        /// </summary>
        Q2931 = 201,
        /// <summary>
        /// Virtual Trunk Group
        /// </summary>
        VirtualTG = 202,
        /// <summary>
        /// SIP Trunk Group
        /// </summary>
        SIPTG = 203,
        /// <summary>
        /// SIP Signaling
        /// </summary>
        SIPSig = 204,
        /// <summary>
        /// CATV Upstream Channel
        /// </summary>
        docsCableUpstreamChannel = 205,
        /// <summary>
        /// Acorn Econet
        /// </summary>
        Econet = 206,
        /// <summary>
        /// FSAN 155Mb Symetrical PON interface
        /// </summary>
        PON155 = 207,
        /// <summary>
        /// FSAN622Mb Symetrical PON interface
        /// </summary>
        PON622 = 208,
        /// <summary>
        /// Transparent bridge interface
        /// </summary>
        Bridge = 209,
        /// <summary>
        /// Interface common to multiple lines
        /// </summary>
        linegroup = 210,
        /// <summary>
        /// voice E&M Feature Group D
        /// </summary>
        voiceEMFGD = 211,
        /// <summary>
        /// voice FGD Exchange Access North American
        /// </summary>
        voiceFGDEANA = 212,
        /// <summary>
        /// voice Direct Inward Dialing
        /// </summary>
        voiceDID = 213,
        /// <summary>
        /// MPEG transport interface
        /// </summary>
        MPEGTransport = 214,
        /// <summary>
        /// 6to4 interface
        /// </summary>
        [Obsolete]
        SixToFour = 215,
        /// <summary>
        /// GTP = GPRS Tunneling Protocol
        /// </summary>
        GTP = 216,
        /// <summary>
        /// Paradyne EtherLoop 1
        /// </summary>
        pdnEtherLoop1 = 217,
        /// <summary>
        ///  Paradyne EtherLoop 2
        /// </summary>
        pdnEtherLoop2 = 218,
        /// <summary>
        /// Optical Channel Group
        /// </summary>
        opticalChannelGroup = 219,
        /// <summary>
        /// HomePNA ITU-T G.989
        /// </summary>
        homepna = 220,
        /// <summary>
        /// Generic Framing Procedure = GFP
        /// </summary>
        gfp = 221,
        /// <summary>
        /// Layer 2 Virtual LAN using Cisco ISL
        /// </summary>
        ciscoISLvlan = 222,
        /// <summary>
        /// Acteleis proprietary MetaLOOP High Speed Link
        /// </summary>
        actelisMetaLOOP = 223,
        /// <summary>
        /// FCIP Link
        /// </summary>
        fcipLink = 224,
        /// <summary>
        /// Resilient Packet Ring Interface Type
        /// </summary>
        rpr = 225,
        /// <summary>
        /// RF Qam Interface
        /// </summary>
        qam = 226,
        /// <summary>
        /// Link Management Protocol
        /// </summary>
        lmp = 227,
        /// <summary>
        /// Cambridge Broadband Networks Limited VectaStar
        /// </summary>
        cblVectaStar = 228,
        /// <summary>
        /// CATV Modular CMTS Downstream Interface
        /// </summary>
        docsCableMCmtsDownstream = 229,
        /// <summary>
        /// Asymmetric Digital Subscriber Loop Version 2
        /// DEPRECATED/OBSOLETED - please use ADSL2Plus(238) instead
        /// </summary>
        [Obsolete]
        ADSL2 = 230,
        /// <summary>
        /// MACSecControlled
        /// </summary>
        macSecControlledIF = 231,
        /// <summary>
        /// MACSecUncontrolled
        /// </summary>
        macSecUncontrolledIF = 232,
        /// <summary>
        /// Avici Optical Ethernet Aggregate
        /// </summary>
        aviciOpticalEther = 233,
        /// <summary>
        /// atmbond
        /// </summary>
        atmbond = 234,
        /// <summary>
        /// voice FGD Operator Services
        /// </summary>
        voiceFGDOS = 235,
        /// <summary>
        /// MultiMedia over Coax Alliance = MoCA Interface as documented in information provided privately to IANA
        /// </summary>
        mocaVersion1 = 236,
        /// <summary>
        /// IEEE 802.16 WMAN interface
        /// </summary>
        ieee80216WMAN = 237,
        /// <summary>
        /// Asymmetric Digital Subscriber Loop Version 2, Version 2 Plus and all variants
        /// </summary>
        ADSL2Plus = 238,
        /// <summary>
        /// DVB-RCS MAC Layer
        /// </summary>
        dvbRcsMacLayer = 239,
        /// <summary>
        /// DVB Satellite TDM
        /// </summary>
        dvbTdm = 240,
        /// <summary>
        /// DVB-RCS TDMA
        /// </summary>
        dvbRcsTdma = 241,
        /// <summary>
        /// LAPS based on ITU-T X.86/Y.1323
        /// </summary>
        x86Laps = 242,
        /// <summary>
        /// 3GPP WWAN
        /// </summary>
        wwanPP = 243,
        /// <summary>
        /// 3GPP2 WWAN
        /// </summary>
        wwanPP2 = 244,
        /// <summary>
        /// voice P-phone EBS physical interface
        /// </summary>
        voiceEBS = 245,
        /// <summary>
        /// Pseudowire interface type
        /// </summary>
        ifPwType = 246,
        /// <summary>
        /// Internal LAN on a bridge per IEEE 802.1ap
        /// </summary>
        ilan = 247,
        /// <summary>
        /// Provider Instance Port on a bridge per IEEE 802.1ah PBB
        /// </summary>
        pip = 248,
        /// <summary>
        /// Alcatel-Lucent Ethernet Link Protection
        /// </summary>
        aluELP = 249,
        /// <summary>
        /// Gigabit-capable passive optical networks = G-PON as per ITU-T G.948
        /// </summary>
        gpon = 250,
        /// <summary>
        /// Very high speed digital subscriber line Version 2  = as per ITU-T Recommendation G.993.2
        /// </summary>
        VDSL2 = 251,
        /// <summary>
        /// WLAN Profile Interface
        /// </summary>
        capwapDot11Profile = 252,
        /// <summary>
        /// WLAN BSS Interface
        /// </summary>
        capwapDot11Bss = 253,
        /// <summary>
        /// WTP Virtual Radio Interface
        /// </summary>
        capwapWtpVirtualRadio = 254,
        /// <summary>
        /// bitsport
        /// </summary>
        bits = 255,
        /// <summary>
        /// DOCSIS CATV Upstream RF Port
        /// </summary>
        docsCableUpstreamRfPort = 256,
        /// <summary>
        /// CATV downstream RF port
        /// </summary>
        cableDownstreamRfPort = 257,
        /// <summary>
        /// VMware Virtual Network Interface
        /// </summary>
        vmwareVirtualNic = 258,
        /// <summary>
        /// IEEE 802.15.4 WPAN interface
        /// </summary>
        ieee802154 = 259,
        /// <summary>
        /// OTN Optical Data Unit
        /// </summary>
        otnOdu = 260,
        /// <summary>
        /// OTN Optical channel Transport Unit
        /// </summary>
        otnOtu = 261,
        /// <summary>
        /// VPLS Forwarding Instance Interface Type
        /// </summary>
        ifVfiType = 262,
        /// <summary>
        /// G.998.1 bonded interface
        /// </summary>
        g9981 = 263,
        /// <summary>
        /// G.998.2 bonded interface
        /// </summary>
        g9982 = 264,
        /// <summary>
        /// G.998.3 bonded interface
        /// </summary>
        g9983 = 265,
        /// <summary>
        /// Ethernet Passive Optical Networks = E-PON
        /// </summary>
        aluEpon = 266,
        /// <summary>
        /// EPON Optical Network Unit
        /// </summary>
        aluEponOnu = 267,
        /// <summary>
        /// EPON physical User to Network interface
        /// </summary>
        aluEponPhysicalUni = 268,
        /// <summary>
        /// The emulation of a point-to-point link over the EPON layer
        /// </summary>
        aluEponLogicalLink = 269,
        /// <summary>
        /// GPON Optical Network Unit
        /// </summary>
        aluGponOnu = 270,
        /// <summary>
        /// GPON physical User to Network interface
        /// </summary>
        aluGponPhysicalUni = 271,
        /// <summary>
        /// VMware NIC Team
        /// </summary>
        vmwareNicTeam = 272,
        /// <summary>
        /// CATV Downstream OFDM interface
        /// </summary>
        docsOfdmDownstream = 277,
        /// <summary>
        /// CATV Upstream OFDMA interface
        /// </summary>
        docsOfdmaUpstream = 278,
        /// <summary>
        /// G.fast port
        /// </summary>
        gfast = 279,
        /// <summary>
        /// SDCI = IO-Link
        /// </summary>
        sdci = 280,
        /// <summary>
        /// Xbox wireless
        /// </summary>
        xboxWireless = 281,
        /// <summary>
        /// FastDSL
        /// </summary>
        FastDSL = 282,
        /// <summary>
        /// Cable SCTE 55-1 OOB Forward Channel
        /// </summary>
        docsCableScte55d1FwdOob = 283,
        /// <summary>
        /// Cable SCTE 55-1 OOB Return Channel
        /// </summary>
        docsCableScte55d1RetOob = 284,
        /// <summary>
        /// Cable SCTE 55-2 OOB Downstream Channel
        /// </summary>
        docsCableScte55d2DsOob = 285,
        /// <summary>
        /// Cable SCTE 55-2 OOB Upstream Channel
        /// </summary>
        docsCableScte55d2UsOob = 286,
        /// <summary>
        /// Cable Narrowband Digital Forward
        /// </summary>
        docsCableNdf = 287,
        /// <summary>
        /// Cable Narrowband Digital Return
        /// </summary>
        docsCableNdr = 288,
        /// <summary>
        /// Packet Transfer Mode
        /// </summary>
        ptm = 289,
        /// <summary>
        /// G.hn port
        /// </summary>
        ghn = 290,
        /// <summary>
        /// Optical Tributary Signal Interface
        /// </summary>
        OTSI = 291
    }
}
