using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDeviceProvider : IDeviceProvider<IBluetoothDevice>, IBluetoothConnectionProvider
    {
        IBluetoothAdapter Adapter { get; }
        Task<IBluetoothDevice> TryGetFromMAC(MACAddress address);
    }
}
