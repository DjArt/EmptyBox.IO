using System;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Interoperability;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        #region Static public functions
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefault() => new BluetoothAdapter(await Windows.Devices.Bluetooth.BluetoothAdapter.GetDefaultAsync());
        #endregion

        #region IBluetoothAdapter interface properties
        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;
        IBluetoothLEDeviceProvider IBluetoothAdapter.LEDeviceProvider => throw new NotImplementedException();
        #endregion

        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public Windows.Devices.Bluetooth.BluetoothAdapter InternalAdapter { get; set; }
        public Windows.Devices.Radios.Radio InternalRadio { get; set; }
        public RadioStatus RadioStatus => InternalRadio.State.ToRadioStatus();
        public ConnectionStatus ConnectionStatus { get; private set; }
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
        #endregion
    }
}
