using System;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Interoperability;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;
using EmptyBox.ScriptRuntime.Results;
using EmptyBox.Collections.Generic;
using EmptyBox.Collections.ObjectModel;
using EmptyBox.IO.Network.Bluetooth;
using System.Collections.Generic;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        #region Static public functions
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefault() => new BluetoothAdapter(await Windows.Devices.Bluetooth.BluetoothAdapter.GetDefaultAsync());
        #endregion

        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        public event DeviceSearcherEventHandler<IBluetoothDevice> DeviceFound;
        public event DeviceSearcherEventHandler<IBluetoothDevice> DeviceLost;
        public event ItemChangeHandler<IBluetoothDevice> ItemAdded;
        public event ItemChangeHandler<IBluetoothDevice> ItemRemoved;
        public event ItemChangeHandler<IBluetoothDevice> ItemChanged;
        #endregion

        #region Public objects
        public Windows.Devices.Bluetooth.BluetoothAdapter InternalAdapter { get; set; }
        public Windows.Devices.Radios.Radio InternalRadio { get; set; }
        public RadioStatus RadioStatus => InternalRadio.State.ToRadioStatus();
        public ConnectionStatus ConnectionStatus { get; private set; }
        public MACAddress Address { get; private set; }
        public string Name { get; private set; }

        IBluetoothDevice IPointedConnectionProvider<IBluetoothDevice>.Address => this;

        public bool SearcherIsActive => throw new NotImplementedException();

        public ITreeNode<IBluetoothDevice> Parent => throw new NotImplementedException();

        public IEnumerable<ITreeNode<IBluetoothDevice>> Items => throw new NotImplementedException();

        public BluetoothClass DeviceClass => throw new NotImplementedException();

        public BluetoothMode Mode => throw new NotImplementedException();

        public DevicePairStatus PairStatus => throw new NotImplementedException();

        public string Path => throw new NotImplementedException();

        ITreeNode<IDevice> ITreeNode<IDevice>.Parent => throw new NotImplementedException();

        IEnumerable<ITreeNode<IDevice>> ITreeNode<IDevice>.Items => throw new NotImplementedException();
        #endregion

        #region Constructors
        internal BluetoothAdapter(Windows.Devices.Bluetooth.BluetoothAdapter adapter)
        {
            async void Initialization()
            {
                try
                {
                    InternalRadio = await InternalAdapter.GetRadioAsync();
                }
                catch
                {
                    InternalRadio = null;
                }
            }

            InternalAdapter = adapter;
            Task init = Task.Run((Action)Initialization);
            Address = new MACAddress(InternalAdapter.BluetoothAddress);
            DeviceProvider = new BluetoothDeviceProvider(this);
            init.Wait();
            if (InternalRadio != null)
            {
                Name = InternalRadio.Name;
            }
        }

        event ItemChangeHandler<IDevice> IObservableTreeNode<IDevice>.ItemAdded
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event ItemChangeHandler<IDevice> IObservableTreeNode<IDevice>.ItemRemoved
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event ItemChangeHandler<IDevice> IObservableTreeNode<IDevice>.ItemChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
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

        #region Public functions
        public void Dispose()
        {
            Close(false);
        }

        public async Task<VoidResult<AccessStatus>> SetRadioStatus(RadioStatus state)
        {
            try
            {
                return new VoidResult<AccessStatus>((await InternalRadio.SetStateAsync(state.ToRadioState())).ToAccessStatus(), null);
            }
            catch (Exception ex)
            {
                return new VoidResult<AccessStatus>(AccessStatus.UnknownError, ex);
            }
        }

        public Task<RefResult<IBluetoothDevice, AccessStatus>> TryGetFromMAC(MACAddress address)
        {
            throw new NotImplementedException();
        }

        public IPointedConnection<IBluetoothDevice, BluetoothPort> CreateConnection(IAccessPoint<IAddress, IPort> accessPoint)
        {
            throw new NotImplementedException();
        }

        public IPointedConnectionListener<IBluetoothDevice, BluetoothPort> CreateConnectionListener(IPort port)
        {
            throw new NotImplementedException();
        }

        public IPointedConnection<IBluetoothDevice> CreateConnection(IAddress address)
        {
            throw new NotImplementedException();
        }

        public IPointedConnectionListener<IBluetoothDevice> CreateConnectionListener()
        {
            throw new NotImplementedException();
        }

        public IConnection<BluetoothPort> CreateConnection(IPort port)
        {
            throw new NotImplementedException();
        }

        IConnectionListener<BluetoothPort> IConnectionProvider<BluetoothPort>.CreateConnectionListener(IPort port)
        {
            throw new NotImplementedException();
        }

        public IConnection CreateConnection()
        {
            throw new NotImplementedException();
        }

        IConnectionListener IConnectionProvider.CreateConnectionListener()
        {
            throw new NotImplementedException();
        }

        public IAsyncCovariantResult<IEnumerable<IBluetoothDevice>> Search()
        {
            throw new NotImplementedException();
        }

        public Task<VoidResult<AccessStatus>> ActivateSearcher()
        {
            throw new NotImplementedException();
        }

        public Task<VoidResult<AccessStatus>> DeactivateSearcher()
        {
            throw new NotImplementedException();
        }

        public Task<RefResult<IEnumerable<BluetoothAccessPoint>, AccessStatus>> GetServices(BluetoothSDPCacheMode cacheMode = BluetoothSDPCacheMode.Cached)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IAddress other)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
