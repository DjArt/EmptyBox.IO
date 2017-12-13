using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Radio;
using Windows.Devices;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Networking.Sockets;
using EmptyBox.IO.Interoperability;
using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Network.Bluetooth;
using Windows.Networking;
using EmptyBox.IO.Network.MAC;
using Windows.Storage.Streams;
using Windows.Devices.Enumeration.Pnp;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        private static BluetoothAdapter _DefaultAdapter = new BluetoothAdapter();

        [StandardRealization]
        public static async Task<BluetoothAdapter> GetDefaultBluetoothAdapter() => _DefaultAdapter;

        async Task<IEnumerable<IBluetoothDevice>> IBluetoothAdapter.FindDevices() => (await FindDevices()).Select(x => (IBluetoothDevice)x);
        IReadOnlyDictionary<BluetoothPort, byte[]> IBluetoothAdapter.ActiveListeners => _ActiveListeners;

        private Dictionary<BluetoothPort, byte[]> _ActiveListeners { get; set; }

        public event BluetoothDeviceWatcher DeviceAdded;
        public event BluetoothDeviceWatcher DeviceRemoved;
        public event BluetoothDeviceWatcher DeviceUpdated;

        public IReadOnlyDictionary<BluetoothPort, byte[]> ActiveListeners => ActiveListeners;
        public RadioStatus RadioStatus => RadioStatus.Unknown;
        public bool DeviceWatcherIsActive { get; private set; }
        public bool ServiceWatcherIsActive { get; private set; }

        internal BluetoothAdapter()
        {
            _ActiveListeners = new Dictionary<BluetoothPort, byte[]>();
        }

        public async Task<IEnumerable<BluetoothDevice>> FindDevices()
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(Constants.AQS_CLASS_GUID + Constants.AQS_BLUETOOTH_GUID);
            List<BluetoothDevice> result = new List<BluetoothDevice>();
            foreach (DeviceInformation device in devices)
            {
                try
                {
                    RfcommDeviceService rds = await RfcommDeviceService.FromIdAsync(device.Id);
                    if (rds != null)
                    {
                        MACAddress address = rds.ConnectionHostName.ToMACAddress();
                        if (!result.Any(x => x.Address == address))
                        {
                            result.Add(new BluetoothDevice(address, device.Name));
                        }
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
            return AccessStatus.Unknown;
        }

        public Task<bool> StartDeviceWatcher()
        {
            throw new NotImplementedException();
        }

        public Task<bool> StopDeviceWatcher()
        {
            throw new NotImplementedException();
        }
    }
}
