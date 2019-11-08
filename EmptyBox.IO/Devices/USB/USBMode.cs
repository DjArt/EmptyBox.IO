using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.USB
{
    [Flags]
    public enum USBMode : byte
    {
        LowSpeed = 0,
        FullSpeed = 1,
        HighSpeed = 2,
        SuperSpeed = 3,
        SuperSpeedPlus = 4,

    }
}
