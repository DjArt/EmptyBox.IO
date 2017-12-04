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

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        private readonly Guid BT = new Guid("{B142FC3E-FA4E-460B-8ABC-072B628B3C70}");
        async Task<IEnumerable<IBluetoothDevice>> IBluetoothAdapter.FindDevices() => (await FindDevices()).Select(x => (IBluetoothDevice)x);

        private static BluetoothAdapter _DefaultAdapter = new BluetoothAdapter();
        public static async Task<BluetoothAdapter> GetDefaultBluetoothAdapter() => _DefaultAdapter;

        public RadioStatus RadioStatus => RadioStatus.Unknown;

        internal BluetoothAdapter()
        {

        }

        public async Task<IEnumerable<BluetoothDevice>> FindDevices()
        {
            DeviceInformationCollection x = await DeviceInformation.FindAllAsync();
            List<BluetoothDevice> result = new List<BluetoothDevice>();
            foreach (DeviceInformation device in x)
            {

            }
            return result;
        }

        public async Task<IEnumerable<BluetoothAccessPoint>> FindServices(BluetoothPort id)
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(id.ToRfcommServiceID()));
            List<BluetoothAccessPoint> result = new List<BluetoothAccessPoint>();
            foreach (DeviceInformation device in devices)
            {
                RfcommDeviceService rds = await RfcommDeviceService.FromIdAsync(device.Id);
                if (rds != null)
                {
                    result.Add(new BluetoothAccessPoint(new MACAddress(), rds.ServiceId.ToBluetoothPort()));
                }
            }
            return result;
        }

        public async Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            return AccessStatus.Unknown;
        }
    }
}
