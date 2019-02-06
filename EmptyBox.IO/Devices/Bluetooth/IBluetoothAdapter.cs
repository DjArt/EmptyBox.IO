using EmptyBox.IO.Access;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.ScriptRuntime.Results;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothAdapter : IRadio, IBluetoothConnectionProvider, IDeviceProvider<IBluetoothDevice>, IBluetoothDevice
    {
        Task<RefResult<IBluetoothDevice, AccessStatus>> TryGetFromMAC(MACAddress address);
    }
}