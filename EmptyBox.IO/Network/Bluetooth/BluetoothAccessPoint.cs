using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public struct BluetoothAccessPoint : IAccessPoint
    {
        IAddress IAccessPoint.Address => Address;
        IPort IAccessPoint.Port => Port;

        public BluetoothAddress Address { get; set; }
        public BluetoothPort Port { get; set; }

        public BluetoothAccessPoint(BluetoothAddress address, BluetoothPort port)
        {
            Address = address;
            Port = port;
        }
    }
}
