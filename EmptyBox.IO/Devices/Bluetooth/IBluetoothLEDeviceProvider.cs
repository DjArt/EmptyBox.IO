using EmptyBox.IO.Network;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothLEDeviceProvider : IDeviceProvider<IBluetoothLEDevice>
    {
        IBluetoothAdapter Adapter { get; }
        Task<IBluetoothDevice> TryGetFromMAC(MACAddress address);
    }
}
