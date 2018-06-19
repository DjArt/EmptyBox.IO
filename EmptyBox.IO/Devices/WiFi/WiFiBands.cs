using System;

namespace EmptyBox.IO.Devices.WiFi
{
    [Flags]
    public enum WiFiBands : byte
    {
        Unknown     = 0b00,
        F_2dot4GHz  = 0b01,
        F_5Ghz      = 0b10
    }
}
