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
using Windows.Storage.Streams;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        private readonly Guid BT = new Guid("{B142FC3E-FA4E-460B-8ABC-072B628B3C70}");

        public static async Task<BluetoothAdapter> GetDefaultBluetoothAdapter()
        {
            return new BluetoothAdapter(await Windows.Devices.Bluetooth.BluetoothAdapter.GetDefaultAsync());
        }

        IReadOnlyDictionary<BluetoothPort, byte[]> IBluetoothAdapter.ActiveListeners => _ActiveListeners;

        private Dictionary<BluetoothPort, byte[]> _ActiveListeners { get; set; }
        private Windows.Devices.Bluetooth.BluetoothAdapter _Adapter { get; set; }
        private Windows.Devices.Radios.Radio _Radio { get; set; }

        public IReadOnlyDictionary<BluetoothPort, byte[]> ActiveListeners => ActiveListeners;
        private RfcommServiceProvider test0;
        private StreamSocketListener test1;

        public event BluetoothDeviceFindedHandler DeviceFinded;
        public event BluetoothServiceFindedHandler ServiceFinded;

        public Windows.Devices.Bluetooth.BluetoothAdapter Adapter => _Adapter;
        public Windows.Devices.Radios.Radio Radio => _Radio;
        public RadioStatus RadioStatus => _Radio.State.ToRadioStatus();
        public bool DeviceWatcherIsActive { get; private set; }
        public bool ServiceWatcherIsActive { get; private set; }

        public BluetoothAdapter(Windows.Devices.Bluetooth.BluetoothAdapter adapter)
        {
            _Adapter = adapter;
            Task init = new Task(Initialization);
            init.Start();
            _ActiveListeners = new Dictionary<BluetoothPort, byte[]>();
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

        public async Task<IEnumerable<BluetoothAccessPoint>> FindServices(BluetoothPort id)
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(id.ToRfcommServiceID()));
            List<BluetoothAccessPoint> result = new List<BluetoothAccessPoint>();
            foreach (DeviceInformation device in devices)
            {
                try
                {
                    RfcommDeviceService rds = await RfcommDeviceService.FromIdAsync(device.Id);
                    if (rds != null)
                    {
                        result.Add(new BluetoothAccessPoint(rds.ConnectionHostName.ToMACAddress(), rds.ServiceId.ToBluetoothPort()));
                    }
                }
                catch
                {

                }
            }
            return result;
        }

        public async Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            return (await _Radio.SetStateAsync(state.ToRadioState())).ToAccessStatus();
        }

        public Task<bool> StartDeviceWatcher()
        {
            throw new NotImplementedException();
        }

        public Task<bool> StopDeviceWatcher()
        {
            throw new NotImplementedException();
        }

        public Task<bool> StartServiceWatcher()
        {
            throw new NotImplementedException();
        }

        public Task<bool> StopServiceWatcher()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StartListener(BluetoothPort port, byte[] sdprecord)
        {
            _ActiveListeners.Add(port, sdprecord);
            test0 = await RfcommServiceProvider.CreateAsync(port.ToRfcommServiceID());
            var sdpWriter = new DataWriter();
            sdpWriter.WriteByte((4 << 3) | 5);
            sdpWriter.WriteByte((byte)"Test".Length);
            sdpWriter.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            sdpWriter.WriteString("Test");
            test0.SdpRawAttributes.Add(0x100, sdpWriter.DetachBuffer());
            test1 = new StreamSocketListener();
            await test1.BindServiceNameAsync(port.ToRfcommServiceID().AsString());
            test0.StartAdvertising(test1);
            return true;
        }

        public async Task<bool> StopListener(BluetoothPort port)
        {
            throw new NotImplementedException();
        }
    }
}
