using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
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
        #region IConnectionProvider interface properties
        IAddress IConnectionProvider.Address => Adapter.Address;
        #endregion

        #region IBluetoothDeviceProvider interface properties
        IBluetoothAdapter IBluetoothDeviceProvider.Adapter => Adapter;
        #endregion

        #region Private objects
        private DeviceWatcher Watcher;
        private List<BluetoothDevice> Cache;
        private object Lock = new object();
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
            Cache = new List<BluetoothDevice>();
            Watcher = DeviceInformation.CreateWatcher(Constants.AQS_CLASS_GUID + Constants.AQS_BLUETOOTH_GUID);
            Watcher.Added += Watcher_Added;
            Watcher.EnumerationCompleted += Watcher_EnumerationCompleted;
            Watcher.Removed += Watcher_Removed;
            Watcher.Stopped += Watcher_Stopped;
            Watcher.Updated += Watcher_Updated;
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
        #endregion Private function

        #region Public functions
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

        public BluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return new BluetoothConnection(this, accessPoint);
        }

        public BluetoothConnectionListener CreateConnectionListener(BluetoothPort port)
        {
            return new BluetoothConnectionListener(this, port);
        }

        public async Task<IBluetoothDevice> TryGetFromMAC(MACAddress address)
        {
            return (await Find()).FirstOrDefault(x => x.Address == address);
        }
        #endregion
    }
}
