using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.HID
{
    [Flags]
    public enum KeyAction : byte
    {
        Down    = 0b00000000,
        Up      = 0b00000001,
        TurnOff = 0b00000010,
        TurnOn  = 0b00000100,
    }
}