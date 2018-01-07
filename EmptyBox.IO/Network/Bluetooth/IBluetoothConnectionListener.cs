using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.MAC;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionListener : IConnectionListener<MACAddress, BluetoothPort, BluetoothAccessPoint, IBluetoothAdapter>
    {

    }
}
