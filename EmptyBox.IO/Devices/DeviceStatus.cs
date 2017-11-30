using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices
{
    [Flags]
    public enum DeviceStatus : byte
    {
        Unknow          = 0b00000000,
        Connected       = 0b00000001,
        Paired          = 0b00000010,
        Aviliable       = 0b00000100,
        Enabled         = 0b00001000,

    }
}
