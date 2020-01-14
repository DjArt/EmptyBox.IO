using System;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Interoperability;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Access;
using EmptyBox.IO.Network.Bluetooth;
using System.Collections.Generic;
using Windows.Devices.Enumeration;
using EmptyBox.IO.Devices.Enumeration;
using EmptyBox.IO.Network.Bluetooth.Classic;
using EmptyBox.ScriptRuntime.Results;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        private const string BLUETOOTH_LE_GUID = "{BB7BB05E-5972-42B5-94FC-76EAA7084D49}";
        private const string BLUETOOTH_GUID = "{E0CBF06C-CD8B-4647-BB8A-263B43F0F974}";
        private const string c0 = "System.Devices.DevObjectType:=5";
        private const string c1 = "System.Devices.Aep.ProtocolId:=";
        private const string query = c0 + " AND (" + c1 + "\"" + BLUETOOTH_GUID + "\" OR " + c1 + "\"" + BLUETOOTH_LE_GUID + "\")";
        private const string query1 = c0 + " AND " + c1 + "\"" + BLUETOOTH_GUID + "\"";
        private const string query2 = c0 + " AND " + c1 + "\"" + BLUETOOTH_LE_GUID + "\"";

        #region Private objects
        private List<BluetoothDevice> _Devices;
        private DeviceWatcher _Watcher;
        #endregion

        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        public event DeviceSearcherEventHandler<IBluetoothDevice> DeviceFound;
        public event DeviceSearcherEventHandler<IBluetoothDevice> DeviceLost;
        #endregion

        #region Public objects
        public Windows.Devices.Bluetooth.BluetoothAdapter InternalAdapter { get; }
        public Windows.Devices.Radios.Radio InternalRadio { get; }
        public RadioStatus RadioStatus => InternalRadio == null ? RadioStatus.Unknown : InternalRadio.State.ToRadioStatus();
        public ConnectionStatus ConnectionStatus { get; private set; }
        public string Name => InternalRadio?.Name;
        public bool IsStarted { get; private set; }
        public BluetoothClass DeviceClass => throw new NotImplementedException();
        public BluetoothMode Mode { get; }
        public BluetoothLowEnergyFeatures LowEnergyFeatures { get; }
        public DevicePairStatus PairStatus => throw new NotSupportedException();
        public MACAddress HardwareAddress { get; }
        IBluetoothDevice IPointedConnectionProvider<IBluetoothDevice>.Address => this;
        public bool SearcherIsActive => throw new NotImplementedException();
        IBluetoothAdapter IBluetoothDevice.Parent => throw new NotImplementedException();


        #endregion

        #region Constructors
        internal BluetoothAdapter(Windows.Devices.Bluetooth.BluetoothAdapter adapter)
        {
            InternalAdapter = adapter;
            ConfiguredTaskAwaitable<Windows.Devices.Radios.Radio> initRadio = InternalAdapter.GetRadioAsync().AsTask().ConfigureAwait(false);
            _Devices = new List<BluetoothDevice>();
            HardwareAddress = new MACAddress(InternalAdapter.BluetoothAddress);
            Mode |= InternalAdapter.IsClassicSupported ? BluetoothMode.Classic : BluetoothMode.Unknown;
            Mode |= InternalAdapter.IsLowEnergySupported ? BluetoothMode.LowEnergy : BluetoothMode.Unknown;
            LowEnergyFeatures |= InternalAdapter.IsAdvertisementOffloadSupported ? BluetoothLowEnergyFeatures.AdvertisementOffloadSupport : BluetoothLowEnergyFeatures.None;
            LowEnergyFeatures |= InternalAdapter.IsCentralRoleSupported ? BluetoothLowEnergyFeatures.CentralRoleSupport : BluetoothLowEnergyFeatures.None;
            LowEnergyFeatures |= InternalAdapter.IsPeripheralRoleSupported ? BluetoothLowEnergyFeatures.PeripheralRoleSupport : BluetoothLowEnergyFeatures.None;
            //LowEnergyFeatures |= InternalAdapter.AreLowEnergySecureConnectionsSupported ? BluetoothLowEnergyFeatures.LowEnergySecureConnectionsSupport : BluetoothLowEnergyFeatures.None;
            _Watcher = DeviceInformation.CreateWatcher(query);
            _Watcher.Added += _Watcher_Added;
            _Watcher.EnumerationCompleted += _Watcher_EnumerationCompleted;
            _Watcher.Removed += _Watcher_Removed;
            _Watcher.Stopped += _Watcher_Stopped;
            _Watcher.Updated += _Watcher_Updated;
            DeviceEnumerator.ConnectionStatusChangedByID += DeviceEnumerator_ConnectionStatusChangedByID;
            InternalRadio = initRadio.GetAwaiter().GetResult();
        }

        private void DeviceEnumerator_ConnectionStatusChangedByID(string sender, ConnectionStatus args)
        {
            if (sender == InternalAdapter.DeviceId && ConnectionStatus != args)
            {
                ConnectionStatus = args;
                ConnectionStatusChanged?.Invoke(this, ConnectionStatus);
            }
        }

        private async Task<BluetoothDevice> _CreateDevice(string id)
        {
            if (id.StartsWith("BluetoothLE"))
            {
                Windows.Devices.Bluetooth.BluetoothLEDevice @internal = await Windows.Devices.Bluetooth.BluetoothLEDevice.FromIdAsync(id);
                BluetoothDevice device = _Devices.Find(x => x.HardwareAddress == new MACAddress(@internal.BluetoothAddress));
                if (device != null)
                {
                    if (device.LEDevice == null)
                    {
                        device.LEDevice = @internal;
                    }
                }
                else
                {
                    device = new BluetoothDevice(this, @internal);
                    _Devices.Add(device);
                }
                return device;
            }
            else if (id.StartsWith("Bluetooth"))
            {
                Windows.Devices.Bluetooth.BluetoothDevice @internal = await Windows.Devices.Bluetooth.BluetoothDevice.FromIdAsync(id);
                BluetoothDevice device = _Devices.Find(x => x.HardwareAddress == new MACAddress(@internal.BluetoothAddress));
                if (device != null)
                {
                    if (device.Device == null)
                    {
                        device.Device = @internal;
                    }
                }
                else
                {
                    device = new BluetoothDevice(this, @internal);
                    _Devices.Add(device);
                }
                return device;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void _Watcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {

        }

        private void _Watcher_Stopped(DeviceWatcher sender, object args)
        {

        }

        private async void _Watcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            try
            {
                DeviceLost?.Invoke(this, await _CreateDevice(args.Id));
            }
            catch
            {

            }
        }

        private void _Watcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {

        }

        private async void _Watcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            try
            {
                DeviceFound?.Invoke(this, await _CreateDevice(args.Id));
            }
            catch
            {

            }
        }
        #endregion

        #region Destructor
        ~BluetoothAdapter()
        {
            Close(false);
        }
        #endregion

        #region Private functions
        private void Close(bool unexcepted)
        {
            //TODO
        }
        #endregion

        IPointedConnection<IBluetoothDevice, BluetoothPort> IPointedConnectionProvider<IBluetoothDevice, BluetoothPort>.CreateConnection(IAccessPoint<IAddress, IPort> accessPoint)
        {
            if (accessPoint is BluetoothClassicAccessPoint bap)
            {
                return CreateConnection(bap);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        IPointedConnectionListener<IBluetoothDevice, BluetoothPort> IPointedConnectionProvider<IBluetoothDevice, BluetoothPort>.CreateConnectionListener(IPort port)
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

        IPointedConnection<IBluetoothDevice> IPointedConnectionProvider<IBluetoothDevice>.CreateConnection(IAddress address) => throw new NotSupportedException();
        IPointedConnectionListener<IBluetoothDevice> IPointedConnectionProvider<IBluetoothDevice>.CreateConnectionListener() => throw new NotSupportedException();
        IConnection<BluetoothPort> IConnectionProvider<BluetoothPort>.CreateConnection(IPort port) => throw new NotSupportedException();
        IConnectionListener<BluetoothPort> IConnectionProvider<BluetoothPort>.CreateConnectionListener(IPort port)
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

        IConnection IConnectionProvider.CreateConnection() => throw new NotSupportedException();
        IConnectionListener IConnectionProvider.CreateConnectionListener() => throw new NotSupportedException();

        #region Public functions
        public void Dispose()
        {
            Close(false);
        }

        public async Task SetRadioStatus(RadioStatus state)
        {
            await InternalRadio.SetStateAsync(state.ToRadioState());
        }

        public async Task<IBluetoothDevice> TryGetFromMAC(MACAddress address)
        {
            return (await Search()).FirstOrDefault(x => x.HardwareAddress == address);
        }

        public BluetoothConnection CreateConnection(BluetoothClassicAccessPoint accessPoint)
        {
            return new BluetoothConnection(this, accessPoint);
        }

        public BluetoothConnectionListener CreateConnectionListener(BluetoothPort port)
        {
            return new BluetoothConnectionListener(this, port);
        }

        public async IAsyncCovariantResult<IEnumerable<IBluetoothDevice>> Search()
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelector());
            List<IBluetoothDevice> result = new List<IBluetoothDevice>();
            foreach (DeviceInformation device in devices)
            {
                result.Add(await _CreateDevice(device.Id));
            }
            return result;
        }

        public Task<IEnumerable<BluetoothClassicAccessPoint>> GetClassicServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            throw new PlatformNotSupportedException();
        }

        public Task<IEnumerable<BluetoothClassicAccessPoint>> GetGATTServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            throw new PlatformNotSupportedException();
        }

        public async Task ActivateSearcher()
        {
            await Task.Yield();
            _Watcher.Start();
            IsStarted = true;
        }

        public async Task DeactivateSearcher()
        {
            await Task.Yield();
            _Watcher.Stop();
            IsStarted = false;
        }

        public bool Equals(IAddress other)
        {
            throw new NotImplementedException();
        }

        IBluetoothConnection IBluetoothConnectionProvider.CreateConnection(IBluetoothAccessPoint<BluetoothPort> accessPoint)
        {
            return CreateConnection(accessPoint as BluetoothClassicAccessPoint);
        }

        IBluetoothConnectionListener IBluetoothConnectionProvider.CreateConnectionListener(BluetoothPort port)
        {
            return CreateConnectionListener(port);
        }
        #endregion
    }
}
