using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDeviceProvider : IDeviceProvider<IBluetoothDevice>
    {
        IBluetoothAdapter Provider { get; }
    }
}
