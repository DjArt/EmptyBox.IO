using EmptyBox.IO.Devices.Bluetooth;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothAccessPoint : IAccessPoint<IBluetoothDevice, BluetoothPort>
    {
        public BluetoothAccessPointType Type { get; set; }
        public IBluetoothDevice Address { get; set; }
        public BluetoothPort Port { get; set; }

        public BluetoothAccessPoint(IBluetoothDevice address, BluetoothPort port, BluetoothAccessPointType type)
        {
            Address = address;
            Port = port;
            Type = type;
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
