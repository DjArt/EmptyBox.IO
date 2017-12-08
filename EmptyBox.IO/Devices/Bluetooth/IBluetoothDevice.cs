using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDevice : IPairableDevice
    {
        string Name { get; }
        MACAddress Address { get; }
        BluetoothLinkType DeviceType { get; }
        BluetoothDeviceClass DeviceClass { get; }
        Task<IEnumerable<BluetoothAccessPoint>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached);
    }
}