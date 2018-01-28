using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothDevice : IBluetoothDevice
    {
        #region Public objects
        public string Name => Device.Name;
        public event DeviceConnectionStatusHandler ConnectionStatusEvent;
        public Windows.Devices.Bluetooth.BluetoothDevice Device { get; private set; }
        public ConnectionStatus ConnectionStatus => Device.ConnectionStatus.ToConnectionStatus();
        public MACAddress Address { get; private set; }
        public DevicePairStatus PairStatus => throw new NotImplementedException();
        public BluetoothDeviceClass DeviceClass => throw new NotImplementedException();
        #endregion
        
        #region Constructors
        public BluetoothDevice(Windows.Devices.Bluetooth.BluetoothDevice device)
        {
            Device = device;
            Address = new MACAddress(Device.BluetoothAddress);
        }
        #endregion

        #region Public functions
        public async Task<IEnumerable<BluetoothAccessPoint>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            RfcommDeviceServicesResult services = await Device.GetRfcommServicesAsync(cacheMode.ToBluetoothCacheMode());
            return services.Services.Select(x => new BluetoothAccessPoint(this, x.ServiceId.ToBluetoothPort()));
        }
        #endregion
    }
}
