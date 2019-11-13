using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.Bluetooth.Classic;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionProvider : IPointedConnectionProvider<IBluetoothDevice, BluetoothPort>
    {
        IBluetoothConnection CreateConnection(IBluetoothAccessPoint<BluetoothPort> accessPoint);
        IBluetoothConnectionListener CreateConnectionListener(BluetoothPort port);
    }
}
