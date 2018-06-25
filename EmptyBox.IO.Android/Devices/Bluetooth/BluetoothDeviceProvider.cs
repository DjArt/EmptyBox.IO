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
using EmptyBox.IO.Access;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.ScriptRuntime;
using EmptyBox.ScriptRuntime.Results;

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

        #region Public events
        public event DeviceProviderEventHandler<IBluetoothDevice> DeviceFound;
        public event DeviceProviderEventHandler<IBluetoothDevice> DeviceLost;
        #endregion

        #region Public objects
        public bool IsStarted => throw new NotImplementedException();
        public BluetoothAdapter Adapter { get; private set; }
        public MACAddress Address => Adapter.Address;
        #endregion

        #region Constructors
        internal BluetoothDeviceProvider(BluetoothAdapter adapter)
        {
            Adapter = Adapter;
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

        #region Public functions
        public async IAsyncCovariantResult<IEnumerable<IBluetoothDevice>> Find()
        {
            await Task.Yield();
            return Adapter.InternalDevice.BondedDevices.Select(x => new BluetoothDevice(x));
        }

        public async Task<VoidResult<AccessStatus>> StartWatcher()
        {
            throw new NotImplementedException();
        }

        public async Task<VoidResult<AccessStatus>> StopWatcher()
        {
            throw new NotImplementedException();
        }

        public BluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return new BluetoothConnection(this, accessPoint);
        }

        public BluetoothConnectionListener CreateConnectionListener(BluetoothPort port)
        {
            return new BluetoothConnectionListener(this, port);
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
        #endregion
    }
}