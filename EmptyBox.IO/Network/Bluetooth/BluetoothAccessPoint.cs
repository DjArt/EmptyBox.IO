using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothAccessPoint : IAccessPoint<IBluetoothDevice, BluetoothPort>
    {
        public IBluetoothDevice Address { get; private set; }
        public BluetoothPort Port { get; private set; }
        public BluetoothMode Mode { get; private set; }

        public BluetoothAccessPoint(IBluetoothDevice address, BluetoothPort port, BluetoothMode mode)
        {
            Address = address;
            Port = port;
            Mode = mode;
        }

        public static bool operator ==(BluetoothAccessPoint x, BluetoothAccessPoint y)
        {
            return x.Address == y.Address && x.Port == y.Port && x.Mode == y.Mode;
        }

        public static bool operator !=(BluetoothAccessPoint x, BluetoothAccessPoint y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (obj is BluetoothAccessPoint point)
            {
                return this == point;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode() ^ Port.GetHashCode() ^ Mode.GetHashCode();
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

        public bool Equals(IAccessPoint<IAddress, IPort> other)
        {
            if (other is BluetoothAccessPoint point)
            {
                return this == point;
            }
            else
            {
                return false;
            }
        }
    }
}
