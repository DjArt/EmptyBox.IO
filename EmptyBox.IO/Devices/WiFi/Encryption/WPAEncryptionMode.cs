using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi.Encryption
{
    [Flags]
    public enum WPAEncryptionMode
    {
        Auto    = 0b00,
        TKIP    = 0b01,
        /// <summary>
        /// AES
        /// </summary>
        CCMP    = 0b10,
        Mixed   = 0b11,
    }
}
