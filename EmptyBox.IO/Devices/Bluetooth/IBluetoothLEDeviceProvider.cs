using EmptyBox.IO.Access;
using EmptyBox.IO.Network;
using EmptyBox.ScriptRuntime.Results;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothLEDeviceProvider : IDeviceProvider<IBluetoothLEDevice>
    {
        IBluetoothAdapter Adapter { get; }
        Task<RefResult<IBluetoothLEDevice, AccessStatus>> TryGetFromMAC(MACAddress address);
    }
}
