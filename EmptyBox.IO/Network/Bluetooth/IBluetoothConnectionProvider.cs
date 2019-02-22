using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionProvider : IPointedConnectionProvider<IBluetoothDevice, BluetoothPort>
    {
        IBluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint);
        IBluetoothConnectionListener CreateConnectionListener(BluetoothPort port);
    }
}
