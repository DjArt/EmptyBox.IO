using EmptyBox.IO.Access;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network;
using System.Threading.Tasks;
using System;
using EmptyBox.ScriptRuntime;
using EmptyBox.ScriptRuntime.Results;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        #region Static private functions
        private static BluetoothAdapter DefaultAdapter = new BluetoothAdapter();
        #endregion

        #region Static public functions
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefault() => DefaultAdapter;
        #endregion

        #region IBluetoothDevice interface objects
        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;
        IBluetoothLEDeviceProvider IBluetoothAdapter.LEDeviceProvider => null;
        #endregion

        #region Public events
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;
        #endregion

        #region Public objects
        public ConnectionStatus ConnectionStatus => ConnectionStatus.Unknow;
        public RadioStatus RadioStatus => RadioStatus.Unknown;
        public BluetoothDeviceProvider DeviceProvider { get; private set; }
        public MACAddress Address => new MACAddress();
        public string Name => "Not supported";
        #endregion

        #region Constructors
        private BluetoothAdapter()
        {
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
            await Task.Yield();
            return new VoidResult<AccessStatus>(AccessStatus.NotSupported, new NotSupportedException("Windows 8.1 is not support changing radio status"));
        }
        #endregion
    }
}
