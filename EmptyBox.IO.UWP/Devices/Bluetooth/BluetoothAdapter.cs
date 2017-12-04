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
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        public static async Task<BluetoothAdapter> GetDefaultBluetoothAdapter()
        {
            return new BluetoothAdapter(await Windows.Devices.Bluetooth.BluetoothAdapter.GetDefaultAsync());
        }

        public Windows.Devices.Bluetooth.BluetoothAdapter Adapter => _Adapter;
        public Windows.Devices.Radios.Radio Radio => _Radio;
        private Windows.Devices.Bluetooth.BluetoothAdapter _Adapter;
        private Windows.Devices.Radios.Radio _Radio;

        public RadioStatus RadioStatus => _Radio.State.ToRadioStatus();

        public BluetoothAdapter(Windows.Devices.Bluetooth.BluetoothAdapter adapter)
        {
            _Adapter = adapter;
            Task init = new Task(Initialization);
            init.Start();
            init.Wait();
        }

        private async void Initialization()
        {
            _Radio = await _Adapter.GetRadioAsync();
        }

        public async Task<IEnumerable<IBluetoothDevice>> FindDevices()
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelector());
            List<IBluetoothDevice> result = new List<IBluetoothDevice>();
            foreach (DeviceInformation device in devices)
            {
                var tmp0 = await Windows.Devices.Bluetooth.BluetoothDevice.FromIdAsync(device.Id);
                result.Add(new BluetoothDevice(tmp0));
            }
            return result;
        }

        public async Task<IEnumerable<IBluetoothService>> FindServices(BluetoothPort id)
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(id.ToRfcommServiceID()));
            List<IBluetoothService> result = new List<IBluetoothService>();
            foreach (DeviceInformation device in devices)
            {
                var tmp0 = await RfcommDeviceService.FromIdAsync(device.Id);
                result.Add(new BluetoothService(tmp0));
            }
            return result;
        }

        public async Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            return (await _Radio.SetStateAsync(state.ToRadioState())).ToAccessStatus();
        }
    }
}
