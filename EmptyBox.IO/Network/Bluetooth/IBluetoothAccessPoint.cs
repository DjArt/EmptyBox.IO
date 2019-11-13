using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothAccessPoint<out TPort> : IAccessPoint<IBluetoothDevice, TPort>
        where TPort : IPort
    {
        BluetoothMode Mode { get; }
        BluetoothProtocol Protocol { get; }
    }
}
