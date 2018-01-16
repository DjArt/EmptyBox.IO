using EmptyBox.IO.Devices.Radio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefault() => new BluetoothAdapter(Android.Bluetooth.BluetoothAdapter.DefaultAdapter);

        IAddress IConnectionProvider.Address => Address;

        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;
        
        public Android.Bluetooth.BluetoothAdapter InternalDevice { get; private set; }
        public IBluetoothConnectionProvider BluetoothProvider => this;
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

        public IBluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint)
        {
            throw new NotImplementedException();
        }

        public IConnection CreateConnection(IAccessPoint accessPoint)
        {
            throw new NotImplementedException();
        }

        public IBluetoothConnectionListener CreateConnectionListener(BluetoothPort port)
        {
            throw new NotImplementedException();
        }

        public IConnectionListener CreateConnectionListener(IPort port)
        {
            throw new NotImplementedException();
        }

        public Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            throw new NotImplementedException();
        }

        IConnection<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>> IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>.CreateConnection(BluetoothAccessPoint accessPoint)
        {
            throw new NotImplementedException();
        }

        IConnectionListener<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>> IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>.CreateConnectionListener(BluetoothPort port)
        {
            throw new NotImplementedException();
        }
    }
}