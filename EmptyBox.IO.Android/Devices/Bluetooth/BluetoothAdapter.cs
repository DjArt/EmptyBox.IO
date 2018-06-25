using EmptyBox.IO.Devices.Radio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        #region Static public functions
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefault() => new BluetoothAdapter(Android.Bluetooth.BluetoothAdapter.DefaultAdapter);
        #endregion

        #region IBluetoothAdapter interface objects
        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;
        IBluetoothLEDeviceProvider IBluetoothAdapter.LEDeviceProvider => null;
        #endregion

        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public Android.Bluetooth.BluetoothAdapter InternalDevice { get; private set; }
        public RadioStatus RadioStatus => InternalDevice.State.ToRadioStatus();
        public ConnectionStatus ConnectionStatus { get; private set; }
        public MACAddress Address { get; private set; }
        public BluetoothDeviceProvider DeviceProvider { get; private set; }
        public string Name => InternalDevice.Name;
        #endregion

        #region Constructors
        internal BluetoothAdapter(Android.Bluetooth.BluetoothAdapter adapter)
        {
            InternalDevice = adapter;
            MACAddress.TryParse(InternalDevice.Address, out MACAddress address);
            Address = address;
            DeviceProvider = new BluetoothDeviceProvider(this);
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

        }
        #endregion

        #region Public functions
        public void Dispose()
        {
            Close(false);
        }

        public async Task<VoidResult<AccessStatus>> SetRadioStatus(RadioStatus state)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}