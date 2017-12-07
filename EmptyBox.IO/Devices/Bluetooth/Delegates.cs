using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public delegate void BluetoothDeviceFindedHandler(IBluetoothAdapter connection, IBluetoothDevice device);
    public delegate void BluetoothServiceFindedHandler(IBluetoothAdapter connection, BluetoothAccessPoint device);
}