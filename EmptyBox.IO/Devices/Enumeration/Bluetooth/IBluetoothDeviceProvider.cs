using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.Devices.Enumeration.Bluetooth
{
    public interface IBluetoothDeviceProvider : IDeviceProvider<IBluetoothDevice>
    {
        IBluetoothAdapter Adapter { get; }
    }
}
