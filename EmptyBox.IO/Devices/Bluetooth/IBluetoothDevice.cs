using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.Bluetooth.Classic;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDevice : IPairableDevice, IAddress
    {
        MACAddress HardwareAddress { get; }
        BluetoothClass DeviceClass { get; }
        BluetoothMode Mode { get; }
        new IBluetoothAdapter Parent { get; }

        Task<IEnumerable<BluetoothClassicAccessPoint>> GetClassicServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached);
        //Task<IEnumerable<BluetoothRFCOMMAccessPoint>> GetGATTServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached);
    }
}