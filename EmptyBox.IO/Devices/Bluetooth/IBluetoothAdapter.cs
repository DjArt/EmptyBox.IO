using EmptyBox.IO.Devices.Radio;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothAdapter : IRadio
    {
        IBluetoothDeviceProvider DeviceProvider { get; }
        IBluetoothLEDeviceProvider LEDeviceProvider { get; }
    }
}
