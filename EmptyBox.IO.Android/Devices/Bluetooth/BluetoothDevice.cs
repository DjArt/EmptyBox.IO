using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDevice : IBluetoothDevice
    {
        public event DeviceConnectionStatusHandler ConnectionStatusEvent;

        public Android.Bluetooth.BluetoothDevice InternalDevice { get; private set; }
        public string Name => InternalDevice.Name;
        public MACAddress Address { get; private set; }
        public BluetoothLinkType DeviceType => throw new NotImplementedException();
        public BluetoothDeviceClass DeviceClass => throw new NotImplementedException();
        public DevicePairStatus PairStatus => throw new NotImplementedException();
        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();

        internal BluetoothDevice(Android.Bluetooth.BluetoothDevice device)
        {
            InternalDevice = device;
            MACAddress.TryParse(InternalDevice.Address, out MACAddress address);
            Address = address;
        }

        public Task<IEnumerable<BluetoothAccessPoint>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            throw new NotImplementedException();
        }
    }
}