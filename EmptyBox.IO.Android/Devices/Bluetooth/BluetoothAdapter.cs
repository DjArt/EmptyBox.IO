using EmptyBox.IO.Devices.Radio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Access;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefault() => new BluetoothAdapter(Android.Bluetooth.BluetoothAdapter.DefaultAdapter);

        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;
        IBluetoothLEDeviceProvider IBluetoothAdapter.LEDeviceProvider => null;
        
        public Android.Bluetooth.BluetoothAdapter InternalDevice { get; private set; }
        public RadioStatus RadioStatus => InternalDevice.State.ToRadioStatus();
        public MACAddress Address { get; private set; }
        public BluetoothDeviceProvider DeviceProvider { get; private set; }
        public string Name => InternalDevice.Name;

        internal BluetoothAdapter(Android.Bluetooth.BluetoothAdapter adapter)
        {
            InternalDevice = adapter;
            MACAddress.TryParse(InternalDevice.Address, out MACAddress address);
            Address = address;
            DeviceProvider = new BluetoothDeviceProvider(this);
        }

        public Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            throw new NotImplementedException();
        }
    }
}