using System;

namespace EmptyBox.IO.Devices.WiFi
{
    //TODO Rework
    [Obsolete]
    [Flags]
    public enum WiFiEncryptionMode
    {
        Open    = 0b0000,
        WEP     = 0b0001,
        WPA     = 0b0010,
        WPA2_Personal   = 0b0100,
        WPA2_Enterprise = 0b1000
    }
}
