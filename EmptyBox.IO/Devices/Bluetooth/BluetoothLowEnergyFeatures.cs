using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Bluetooth
{
    [Flags]
    public enum BluetoothLowEnergyFeatures
    {
        None = 0b0000,
        LowEnergySecureConnectionsSupport = 0b0001,
        PeripheralRoleSupport = 0b0010,
        CentralRoleSupport = 0b0100,
        AdvertisementOffloadSupport = 0b1000,
    }
}
