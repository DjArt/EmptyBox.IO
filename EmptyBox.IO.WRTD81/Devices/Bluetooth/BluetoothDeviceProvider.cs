using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.MAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDeviceProvider : IBluetoothDeviceProvider
    {
        IBluetoothAdapter IBluetoothDeviceProvider.Provider => Provider;

        private DeviceWatcher Watcher;
        private List<BluetoothDevice> Cache;
        private object Lock = new object();

        public event EventHandler<IBluetoothDevice> DeviceAdded;
        public event EventHandler<IBluetoothDevice> DeviceRemoved;

        public BluetoothAdapter Provider { get; private set; }
        public bool IsStarted { get; private set; }

        internal BluetoothDeviceProvider(BluetoothAdapter provider)
        {
            Provider = provider;
            Cache = new List<BluetoothDevice>();
            Watcher = DeviceInformation.CreateWatcher(Constants.AQS_CLASS_GUID + Constants.AQS_BLUETOOTH_GUID);
            Watcher.Added += Watcher_Added;
            Watcher.EnumerationCompleted += Watcher_EnumerationCompleted;
            Watcher.Removed += Watcher_Removed;
            Watcher.Stopped += Watcher_Stopped;
            Watcher.Updated += Watcher_Updated;
        }

        private async void Watcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {

        }

        private void Watcher_Stopped(DeviceWatcher sender, object args)
        {
            
        }

        private void Watcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            
        }

        private void Watcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            
        }

        private async void Watcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            try
            {
                RfcommDeviceService rds = await RfcommDeviceService.FromIdAsync(args.Id);
                if (rds != null)
                {
                    lock (Lock)
                    {
                        MACAddress address = rds.ConnectionHostName.ToMACAddress();
                        if (!Cache.Any(x => x.Address == address))
                        {
                            BluetoothDevice device = new BluetoothDevice(address, args.Name);
                            Cache.Add(device);
                            DeviceAdded?.Invoke(this, device);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public async Task<IEnumerable<IBluetoothDevice>> Find()
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(Constants.AQS_CLASS_GUID + Constants.AQS_BLUETOOTH_GUID);
            List<BluetoothDevice> result = new List<BluetoothDevice>();
            foreach (DeviceInformation device in devices)
            {
                try
                {
                    RfcommDeviceService rds = await RfcommDeviceService.FromIdAsync(device.Id);
                    if (rds != null)
                    {
                        MACAddress address = rds.ConnectionHostName.ToMACAddress();
                        if (!result.Any(x => x.Address == address))
                        {
                            result.Add(new BluetoothDevice(address, device.Name));
                        }
                    }
                }
                catch
                {

                }
            }
            return result;
        }

        public void StartWatcher()
        {
            Watcher.Start();
            IsStarted = true;
        }

        public void StopWatcher()
        {
            Watcher.Stop();
            IsStarted = false;
            Cache.Clear();
        }
    }
}
