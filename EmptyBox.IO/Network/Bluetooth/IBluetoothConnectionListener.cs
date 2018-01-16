using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothConnectionListener : IConnectionListener<MACAddress, BluetoothPort, BluetoothAccessPoint, IBluetoothAdapter>
    {

    }
}
