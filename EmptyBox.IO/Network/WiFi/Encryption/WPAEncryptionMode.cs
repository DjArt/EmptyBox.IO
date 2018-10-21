using System;

namespace EmptyBox.IO.Network.WiFi.Encryption
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
