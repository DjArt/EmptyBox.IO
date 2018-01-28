using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionProvider : IConnectionProvider
    {
        new MACAddress Address { get; }
        IBluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint);
        IBluetoothConnectionListener CreateConnectionListener(BluetoothPort port);
    }
}
