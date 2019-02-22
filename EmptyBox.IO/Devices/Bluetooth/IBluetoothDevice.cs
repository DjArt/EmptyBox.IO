using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDevice : IPairableDevice, IAddress
    {
        MACAddress HardwareAddress { get; }
        BluetoothClass DeviceClass { get; }
        BluetoothMode Mode { get; }
        new IBluetoothAdapter Parent { get; }

        Task<RefResult<IEnumerable<BluetoothAccessPoint>, AccessStatus>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached);
    }
}