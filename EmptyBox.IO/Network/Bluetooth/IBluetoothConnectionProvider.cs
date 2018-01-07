using EmptyBox.IO.Network.MAC;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionProvider : IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>
    {
        new MACAddress Address { get; }
        new IBluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint);
        new IBluetoothConnectionListener CreateConnectionListener(BluetoothPort port);
    }
}
