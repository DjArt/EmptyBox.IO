using System;
using System.Collections.Generic;
using System.Linq;
using EmptyBox.IO.Devices.Radio;
using Windows.Devices.Enumeration;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Access;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        #region Public static functions
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefaultBluetoothAdapter() => new BluetoothAdapter(await Windows.Devices.Bluetooth.BluetoothAdapter.GetDefaultAsync());
        #endregion

        #region IBluetoothAdapter interface properties
        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;
        IBluetoothLEDeviceProvider IBluetoothAdapter.LEDeviceProvider => throw new NotImplementedException();
        #endregion

        #region Private objects
        private Windows.Devices.Bluetooth.BluetoothAdapter _Adapter { get; set; }
        private Windows.Devices.Radios.Radio _Radio { get; set; }
        #endregion

        #region Public objects
        public RadioStatus RadioStatus => _Radio.State.ToRadioStatus();
        public MACAddress Address { get; private set; }
        public BluetoothDeviceProvider DeviceProvider { get; private set; }
        public string Name { get; private set; }
        #endregion

        #region Constructors
        internal BluetoothAdapter(Windows.Devices.Bluetooth.BluetoothAdapter adapter)
        {
            async void Initialization()
            {
                try
                {
                    _Radio = await _Adapter.GetRadioAsync();
                }
                catch
                {
                    _Radio = null;
                }
            }

            _Adapter = adapter;
            Task init = Task.Run((Action)Initialization);
            Address = new MACAddress(_Adapter.BluetoothAddress);
            DeviceProvider = new BluetoothDeviceProvider(this);
            init.Wait();
            if (_Radio != null)
            {
                Name = _Radio.Name;
            }
        }


        #endregion

        #region Public functions
        public async Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            try
            {
                return (await _Radio.SetStateAsync(state.ToRadioState())).ToAccessStatus();
            }
            catch
            {
                return AccessStatus.Unknown;
            }
        }
        #endregion
    }
}
