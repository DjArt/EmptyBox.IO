using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public enum BluetoothProtocol : ushort
    {
        Unknown = 0x0000,
        SDP = 0x0001,
        UDP = 0x0002,
        RFCOMM = 0x0003,
        TCP = 0x0004,
        [Obsolete]
        TCS_BIN = 0x0005,
        TCS_AT = 0x0006,
        ATT = 0x0007,
        OBEX = 0x0008,
        IP = 0x0009,
        FTP = 0x000A,
        HTTP = 0x000C,
        WSP = 0x000E,
        BNEP = 0x000F,
        [Obsolete]
        UPNP = 0x0010,
        HIDP = 0x0011,
        HardcopyControlChannel = 0x0012,
        HardcopyDataChannel = 0x0014,
        HardcopyNotification = 0x0016,
        AVCTP = 0x0017,
        AVDTP = 0x0019,
        [Obsolete]
        CMTP = 0x001B,
        MCAPControlChannel = 0x001E,
        MCAPDataChannel = 0x001F,
        L2CAP = 0x0100,
    }
}
