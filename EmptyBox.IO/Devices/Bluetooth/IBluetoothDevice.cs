using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDevice : IPairableDevice, IAddress
    {
        MACAddress Address { get; }
        BluetoothDeviceClass DeviceClass { get; }
        Task<IEnumerable<BluetoothAccessPoint>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached);
    }
}