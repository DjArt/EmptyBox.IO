using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Devices.Enumeration.Bluetooth;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.MAC;
using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothAdapter : IRadio, IBluetoothConnectionProvider
    {
        IBluetoothDeviceProvider DeviceProvider { get; }
    }
}
