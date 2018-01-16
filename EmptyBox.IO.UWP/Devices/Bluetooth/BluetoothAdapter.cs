using System;
using System.Collections.Generic;
using System.Linq;
using EmptyBox.IO.Devices.Radio;
using Windows.Devices.Enumeration;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefaultBluetoothAdapter() => new BluetoothAdapter(await Windows.Devices.Bluetooth.BluetoothAdapter.GetDefaultAsync());

        IBluetoothDeviceProvider IBluetoothAdapter.DeviceProvider => DeviceProvider;

        IAddress IConnectionProvider.Address => throw new NotImplementedException();
        
        private Windows.Devices.Bluetooth.BluetoothAdapter _Adapter { get; set; }
        private Windows.Devices.Radios.Radio _Radio { get; set; }
        
        public RadioStatus RadioStatus => _Radio.State.ToRadioStatus();
        public MACAddress Address { get; private set; }
        public IBluetoothConnectionProvider BluetoothProvider => this;
        public BluetoothDeviceProvider DeviceProvider { get; private set; }
        public string Name { get; private set; }

        internal BluetoothAdapter(Windows.Devices.Bluetooth.BluetoothAdapter adapter)
        {
            async void Initialization()
            {
                _Radio = await _Adapter.GetRadioAsync();
            }

            _Adapter = adapter;
            Task init = Task.Run((Action)Initialization);
            Address = new MACAddress(_Adapter.BluetoothAddress);
            DeviceProvider = new BluetoothDeviceProvider(this);
            Name = _Radio.Name;
            init.Wait();
        }

        public async Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            return (await _Radio.SetStateAsync(state.ToRadioState())).ToAccessStatus();
        }

        public BluetoothConnection CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return new BluetoothConnection(this, accessPoint);
        }

        public BluetoothConnectionListener CreateConnectionListener(BluetoothPort port)
        {
            return new BluetoothConnectionListener(this, port);
        }

        #region Реализация методов IBluetoothConnectionProvider
        IBluetoothConnection IBluetoothConnectionProvider.CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return CreateConnection(accessPoint);
        }

        IBluetoothConnectionListener IBluetoothConnectionProvider.CreateConnectionListener(BluetoothPort port)
        {
            return CreateConnectionListener(port);
        }
        #endregion

        #region Реализация методов IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>
        IConnection<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>> IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>.CreateConnection(BluetoothAccessPoint accessPoint)
        {
            return (IConnection<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>>)CreateConnection(accessPoint);
        }

        IConnectionListener<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>> IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>.CreateConnectionListener(BluetoothPort port)
        {
            return (IConnectionListener<MACAddress, BluetoothPort, BluetoothAccessPoint, IConnectionProvider<MACAddress, BluetoothPort, BluetoothAccessPoint>>)CreateConnectionListener(port);
        }
        #endregion

        #region Реализация методов IConnectionProvider
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
        #endregion
    }
}
