using System;

namespace EmptyBox.IO.Network.WiFi.Encryption
{
    [Flags]
    public enum WPAEncryptionVersion
    {
        Unknown     = 0b00,
        WPA         = 0b01,
        WPA2        = 0b10,
        Mixed       = 0b11
    }
}
