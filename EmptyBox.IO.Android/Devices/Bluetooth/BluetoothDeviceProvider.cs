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

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDeviceProvider : IBluetoothDeviceProvider
    {
        IBluetoothAdapter IBluetoothDeviceProvider.Provider => Provider;
        
        public event EventHandler<IBluetoothDevice> DeviceAdded;
        public event EventHandler<IBluetoothDevice> DeviceRemoved;

        public bool IsStarted => throw new NotImplementedException();
        public BluetoothAdapter Provider { get; private set; }

        internal BluetoothDeviceProvider(BluetoothAdapter provider)
        {
            Provider = provider;
        }

        public async Task<IEnumerable<IBluetoothDevice>> Find()
        {
            await Task.Yield();
            return Provider.InternalDevice.BondedDevices.Select(x => new BluetoothDevice(x));
        }

        public void StartWatcher()
        {
            throw new NotImplementedException();
        }

        public void StopWatcher()
        {
            throw new NotImplementedException();
        }
    }
}