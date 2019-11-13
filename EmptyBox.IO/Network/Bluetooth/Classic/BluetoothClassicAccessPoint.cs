using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth.Classic
{
    public sealed class BluetoothClassicAccessPoint : IBluetoothAccessPoint<BluetoothPort>
    {
        public IBluetoothDevice Address { get; private set; }
        public BluetoothPort Port { get; private set; }
        public BluetoothMode Mode => BluetoothMode.Classic;
        public BluetoothProtocol Protocol { get; private set; }

        public BluetoothClassicAccessPoint(IBluetoothDevice address, BluetoothPort port)
        {
            Address = address;
            Port = port;
        }

        public static bool operator ==(BluetoothClassicAccessPoint x, BluetoothClassicAccessPoint y)
        {
            return x.Address == y.Address && x.Port == y.Port && x.Mode == y.Mode;
        }

        public static bool operator !=(BluetoothClassicAccessPoint x, BluetoothClassicAccessPoint y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (obj is BluetoothClassicAccessPoint point)
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
            if (other is BluetoothClassicAccessPoint point)
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
