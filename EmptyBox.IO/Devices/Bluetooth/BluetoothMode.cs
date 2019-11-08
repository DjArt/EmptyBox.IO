using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Bluetooth
{
    [Flags]
    public enum BluetoothMode : byte
    {
        Standard    = 0b001,
        LowEnergy   = 0b010,
        AMP         = 0b100,
    }
}
