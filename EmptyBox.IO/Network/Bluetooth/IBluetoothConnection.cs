using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnection : IConnection<MACAddress, BluetoothPort, BluetoothAccessPoint, IBluetoothAdapter>
    {

    }
}
