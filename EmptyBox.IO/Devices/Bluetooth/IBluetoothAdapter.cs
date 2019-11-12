using EmptyBox.IO.Access;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothAdapter : IRadio, IPointedConnectionProvider<IBluetoothDevice, BluetoothPort>, IDeviceSearcher<IBluetoothDevice>, IBluetoothDevice
    {
        Task<IBluetoothDevice> TryGetFromMAC(MACAddress address);
    }
}