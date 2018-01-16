using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothDevice : IBluetoothDevice
    {
        public string Name => Device.Name;
        public BluetoothLinkType DeviceType { get; protected set; }
        public event DeviceConnectionStatusHandler ConnectionStatusEvent;
        public Windows.Devices.Bluetooth.BluetoothDevice Device { get; private set; }
        public ConnectionStatus ConnectionStatus => Device.ConnectionStatus.ToConnectionStatus();
        public MACAddress Address { get; private set; }
        public DevicePairStatus PairStatus => throw new NotImplementedException();
        public BluetoothDeviceClass DeviceClass => throw new NotImplementedException();

        public BluetoothDevice(Windows.Devices.Bluetooth.BluetoothDevice device)
        {
            Device = device;
            Address = new MACAddress(Device.BluetoothAddress);
        }

        public async Task<IEnumerable<BluetoothAccessPoint>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            RfcommDeviceServicesResult services = await Device.GetRfcommServicesAsync(cacheMode.ToBluetoothCacheMode());
            return services.Services.Select(x => new BluetoothAccessPoint(Address, x.ServiceId.ToBluetoothPort()));
        }
    }
}
