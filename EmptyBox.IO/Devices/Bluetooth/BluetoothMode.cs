using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Bluetooth
{
    [Flags]
    public enum BluetoothMode : byte
    {
        Unknown =   0b00,
        Standard =  0b01,
        LowEnergy = 0b10,
    }
}
