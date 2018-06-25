using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothLEDevice : IPairableDevice, IAddress
    {
        MACAddress Address { get; }
        BluetoothClass DeviceClass { get; }
        Task<RefResult<IEnumerable<BluetoothAccessPoint>, AccessStatus>> GetServices(BluetoothSDPCacheMode cacheMode);
    }
}