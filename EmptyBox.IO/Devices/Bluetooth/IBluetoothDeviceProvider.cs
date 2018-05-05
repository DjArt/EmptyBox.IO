using EmptyBox.IO.Access;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.ScriptRuntime;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDeviceProvider : IDeviceProvider<IBluetoothDevice>, IBluetoothConnectionProvider
    {
        IBluetoothAdapter Adapter { get; }
        Task<RefResult<IBluetoothDevice, AccessStatus>> TryGetFromMAC(MACAddress address);
    }
}
