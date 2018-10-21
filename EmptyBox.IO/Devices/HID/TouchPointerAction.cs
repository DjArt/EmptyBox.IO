using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.HID
{
    public enum TouchPointerAction : byte
    {
        Detected = 0,
        Lost = 1
    }
}
