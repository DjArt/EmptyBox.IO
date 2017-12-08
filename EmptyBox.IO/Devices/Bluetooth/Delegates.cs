using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public delegate void BluetoothDeviceWatcher(IBluetoothAdapter connection, IBluetoothDevice device);
    public delegate void BluetoothServiceWatcher(IBluetoothAdapter connection, BluetoothAccessPoint device);
}