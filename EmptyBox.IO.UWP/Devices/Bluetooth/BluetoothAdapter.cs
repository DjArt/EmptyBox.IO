using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Radio;
using Windows.Devices.Radios;
using Windows.Devices;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Networking.Sockets;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        protected Windows.Devices.Bluetooth.BluetoothAdapter _Adapter;
        protected Windows.Devices.Radios.Radio _Radio;

        public BluetoothAdapter(Windows.Devices.Bluetooth.BluetoothAdapter adapter)
        {
            _Adapter = adapter;
            Initialization();
        }

        protected async void Initialization()
        {
            _Radio = await _Adapter.GetRadioAsync();
        }

        public Task<List<IBluetoothDevice>> FindDevices()
        {
            return null;
        }

        public Task<DeviceStatus> GetDeviceStatus()
        {
            throw new NotImplementedException();
        }

        public Task<RadioStatus> GetRadioStatus()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetRadioStatus(RadioStatus state)
        {
            throw new NotImplementedException();
        }
    }
}
