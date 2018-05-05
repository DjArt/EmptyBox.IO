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
using EmptyBox.IO.Network;
using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothDevice : IBluetoothDevice
    {
        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public Android.Bluetooth.BluetoothDevice InternalDevice { get; private set; }
        public string Name => InternalDevice.Name;
        public MACAddress Address { get; private set; }
        public BluetoothClass DeviceClass => throw new NotImplementedException();
        public DevicePairStatus PairStatus => throw new NotImplementedException();
        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();
        #endregion

        #region Constructors
        internal BluetoothDevice(Android.Bluetooth.BluetoothDevice device)
        {
            InternalDevice = device;
            MACAddress.TryParse(InternalDevice.Address, out MACAddress address);
            Address = address;
        }
        #endregion

        #region Public functions
        public void Dispose()
        {

        }

        public async Task<RefResult<IEnumerable<BluetoothAccessPoint>, AccessStatus>> GetServices(BluetoothSDPCacheMode cacheMode)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}