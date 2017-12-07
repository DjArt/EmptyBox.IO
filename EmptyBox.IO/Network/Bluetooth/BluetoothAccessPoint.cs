using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.MAC;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public struct BluetoothAccessPoint : IAccessPoint
    {
        IAddress IAccessPoint.Address => Address;
        IPort IAccessPoint.Port => Port;

        public MACAddress Address { get; set; }
        public BluetoothPort Port { get; set; }

        public BluetoothAccessPoint(MACAddress address, BluetoothPort port)
        {
            Address = address;
            Port = port;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("(");
            result.Append(Address);
            result.Append(")");
            result.Append(":");
            result.Append(Port);
            return result.ToString();
        }
    }
}
