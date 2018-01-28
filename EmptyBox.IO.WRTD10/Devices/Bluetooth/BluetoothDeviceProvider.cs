using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDeviceProvider : IBluetoothDeviceProvider
    {
        #region IBluetoothDeviceProvider interface properties
        IBluetoothAdapter IBluetoothDeviceProvider.Adapter => Adapter;
        #endregion

        #region IConnectionProvider interface properties
        IAddress IConnectionProvider.Address => Adapter.Address;
        #endregion

        #region Private objects
        private DeviceWatcher Watcher;
        #endregion

        #region Public objects
        public event EventHandler<IBluetoothDevice> DeviceAdded;
        public event EventHandler<IBluetoothDevice> DeviceRemoved;

        public BluetoothAdapter Adapter { get; private set; }
        public MACAddress Address => Adapter.Address;
        public bool IsStarted { get; private set; }
        #endregion

        #region Constructors
        internal BluetoothDeviceProvider(BluetoothAdapter adapter)
        {
            Adapter = adapter;
            Watcher = DeviceInformation.CreateWatcher(Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelector());
        }
        #endregion

        #region IBluetoothConnectionProvider interface properties
        IBluetoothConnection IBluetoothConnectionProvider.CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return CreateConnection(accessPoint);
        }

        IBluetoothConnectionListener IBluetoothConnectionProvider.CreateConnectionListener(BluetoothPort port)
        {
            return CreateConnectionListener(port);
        }
        #endregion

        #region IConnectionProvider interface functions
        IConnection IConnectionProvider.CreateConnection(IAccessPoint accessPoint)
        {
            if (accessPoint is BluetoothAccessPoint point)
            {
                return CreateConnection(point);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        IConnectionListener IConnectionProvider.CreateConnectionListener(IPort port)
        {
            if (port is BluetoothPort _port)
            {
                return CreateConnectionListener(_port);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        #endregion

        #region Private functions
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
        #endregion

        #region Public functions
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

        public async Task<IBluetoothDevice> TryGetFromMAC(MACAddress address)
        {
            return (await Find()).FirstOrDefault(x => x.Address == address);
        }

        public BluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return new BluetoothConnection(this, accessPoint);
        }

        public BluetoothConnectionListener CreateConnectionListener(BluetoothPort port)
        {
            return new BluetoothConnectionListener(this, port);
        }
        #endregion
    }
}
