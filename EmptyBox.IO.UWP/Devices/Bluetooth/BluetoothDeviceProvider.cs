using EmptyBox.IO.Access;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.ScriptRuntime;
using EmptyBox.ScriptRuntime.Results;
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

        #region Public events
        public event DeviceSearcherEventHandler<IBluetoothDevice> DeviceFound;
        public event DeviceSearcherEventHandler<IBluetoothDevice> DeviceLost;
        #endregion

        #region Public objects
        public BluetoothAdapter Adapter { get; private set; }
        public MACAddress Address => Adapter.Address;
        public bool IsStarted { get; private set; }
        #endregion

        #region Constructors
        internal BluetoothDeviceProvider(BluetoothAdapter adapter)
        {
            Adapter = adapter;
            Watcher = DeviceInformation.CreateWatcher(Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelector());
            Watcher.Added += Watcher_Added;
            Watcher.EnumerationCompleted += Watcher_EnumerationCompleted;
            Watcher.Removed += Watcher_Removed;
            Watcher.Stopped += Watcher_Stopped;
            Watcher.Updated += Watcher_Updated;
        }
        #endregion

        #region IBluetoothConnectionProvider interface functions
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
                DeviceLost?.Invoke(this, new BluetoothDevice(await Windows.Devices.Bluetooth.BluetoothDevice.FromIdAsync(args.Id)));
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
                DeviceFound?.Invoke(this, new BluetoothDevice(await Windows.Devices.Bluetooth.BluetoothDevice.FromIdAsync(args.Id)));
            }
            catch
            {

            }
        }
        #endregion

        #region Public functions
        public async IAsyncCovariantResult<IEnumerable<IBluetoothDevice>> Find()
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

        public async Task<VoidResult<AccessStatus>> StartWatcher()
        {
            await Task.Yield();
            Watcher.Start();
            IsStarted = true;
            return new VoidResult<AccessStatus>(AccessStatus.Success, null);
        }

        public async Task<VoidResult<AccessStatus>> StopWatcher()
        {
            await Task.Yield();
            Watcher.Stop();
            IsStarted = false;
            return new VoidResult<AccessStatus>(AccessStatus.Success, null);
        }

        public async Task<RefResult<IBluetoothDevice, AccessStatus>> TryGetFromMAC(MACAddress address)
        {
            try
            {
                IBluetoothDevice device = (await Find()).FirstOrDefault(x => x.Address == address);
                return new RefResult<IBluetoothDevice, AccessStatus>(device, AccessStatus.Success, null);
            }
            catch (Exception ex)
            {
                return new RefResult<IBluetoothDevice, AccessStatus>(null, AccessStatus.UnknownError, ex);
            }
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
