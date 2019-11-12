using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDevice : IPairableDevice, IAddress
    {
        MACAddress HardwareAddress { get; }
        BluetoothClass DeviceClass { get; }
        BluetoothMode Mode { get; }
        new IBluetoothAdapter Parent { get; }

        Task<IEnumerable<BluetoothAccessPoint>> GetRFCOMMServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached);
        Task<IEnumerable<BluetoothAccessPoint>> GetGATTServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached);
    }
}