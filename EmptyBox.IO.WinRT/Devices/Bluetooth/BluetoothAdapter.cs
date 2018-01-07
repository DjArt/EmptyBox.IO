using EmptyBox.IO.Devices.Enumeration.Bluetooth;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public sealed class BluetoothAdapter : IBluetoothAdapter
    {
        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;
        IAddress IConnectionProvider.Address => Address;

        private static BluetoothAdapter _DefaultAdapter = new BluetoothAdapter();

        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefaultBluetoothAdapter() => _DefaultAdapter;
        
        public RadioStatus RadioStatus => RadioStatus.Unknown;
        public BluetoothDeviceProvider DeviceProvider { get; private set; }
        public MACAddress Address { get; private set; }


        private BluetoothAdapter()
        {
            DeviceProvider = new BluetoothDeviceProvider(this);
        }

        public async Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            await Task.Yield();
            return AccessStatus.Unknown;
        }

        public BluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return new BluetoothConnection(this, accessPoint);
        }

        public BluetoothConnectionListener CreateConnectionListener(BluetoothPort port)
        {
            return new BluetoothConnectionListener(this, port);
        }

        IBluetoothConnection IBluetoothConnectionProvider.CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return CreateConnection(accessPoint);
        }

        IBluetoothConnectionListener IBluetoothConnectionProvider.CreateConnectionListener(BluetoothPort port)
        {
            return CreateConnectionListener(port);
        }

        IConnection<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>> IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>.CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return (IConnection<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>>)CreateConnection(accessPoint);
        }

        IConnectionListener<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>> IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>.CreateConnectionListener(BluetoothPort port)
        {
            return (IConnectionListener<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>>)CreateConnectionListener(port);
        }

        IConnection IConnectionProvider.CreateConnection(IAccessPoint accessPoint)
        {
            if (accessPoint is BluetoothAccessPoint)
            {
                return CreateConnection((BluetoothAccessPoint)accessPoint);
            }
            else
            {
                return null;
            }
        }

        IConnectionListener IConnectionProvider.CreateConnectionListener(IPort port)
        {
            if (port is BluetoothPort)
            {
                return CreateConnectionListener((BluetoothPort)port);
            }
            else
            {
                return null;
            }
        }
    }
}
