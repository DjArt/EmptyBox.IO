using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionProvider : IPointedConnectionProvider<IBluetoothDevice, BluetoothPort>
    {
        new MACAddress Address { get; }
        IBluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint);
        IBluetoothConnectionListener CreateConnectionListener(BluetoothPort port);
    }
}
