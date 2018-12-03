using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionListener : IPointedConnectionListener<IBluetoothDevice, BluetoothPort>
    {
        new IBluetoothConnectionProvider ConnectionProvider { get; }
    }
}
