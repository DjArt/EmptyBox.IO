using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi.Encryption
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
