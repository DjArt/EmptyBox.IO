using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDeviceProvider : IBluetoothDeviceProvider
    {
        IBluetoothAdapter IBluetoothDeviceProvider.Provider => Provider;

        private DeviceWatcher Watcher;

        public event EventHandler<IBluetoothDevice> DeviceAdded;
        public event EventHandler<IBluetoothDevice> DeviceRemoved;

        public BluetoothAdapter Provider { get; private set; }
        public bool IsStarted { get; private set; }

        internal BluetoothDeviceProvider(BluetoothAdapter provider)
        {
            Provider = provider;
            Watcher = DeviceInformation.CreateWatcher(Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelector());
        }

        private void Watcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {

        }

        private void Watcher_Stopped(DeviceWatcher sender, object args)
        {

        }

        private async void Watcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            try
            {
                DeviceRemoved?.Invoke(this, new BluetoothDevice(await Windows.Devices.Bluetooth.BluetoothDevice.FromIdAsync(args.Id)));
            }
            catch
            {

            }
        }

        private void Watcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {

        }

        private async void Watcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            try
            {
                DeviceAdded?.Invoke(this, new BluetoothDevice(await Windows.Devices.Bluetooth.BluetoothDevice.FromIdAsync(args.Id)));
            }
            catch
            {

            }
        }

        public async Task<IEnumerable<IBluetoothDevice>> Find()
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelector());
            List<IBluetoothDevice> result = new List<IBluetoothDevice>();
            foreach (DeviceInformation device in devices)
            {
                var tmp0 = await Windows.Devices.Bluetooth.BluetoothDevice.FromIdAsync(device.Id);
                result.Add(new BluetoothDevice(tmp0));
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
        }
    }
}
