using EmptyBox.IO.Access;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefault() => DefaultAdapter;

        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;
        IBluetoothLEDeviceProvider IBluetoothAdapter.LEDeviceProvider => null;

        private static BluetoothAdapter DefaultAdapter = new BluetoothAdapter();
        
        public RadioStatus RadioStatus => RadioStatus.Unknown;
        public BluetoothDeviceProvider DeviceProvider { get; private set; }
        public MACAddress Address => new MACAddress();
        public string Name => "Not supported";

        private BluetoothAdapter()
        {
            DeviceProvider = new BluetoothDeviceProvider(this);
        }

        public async Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            await Task.Yield();
            return AccessStatus.Unknown;
        }
    }
}
