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

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        public static async Task<BluetoothAdapter> GetDefaultBluetoothAdapter() => new BluetoothAdapter();

        public RadioStatus RadioStatus => RadioStatus.Unknown;

        private BluetoothAdapter()
        {

        }

        public async Task<IEnumerable<IBluetoothDevice>> FindDevices()
        {
            throw new PlatformNotSupportedException();
        }

        public async Task<IEnumerable<IBluetoothService>> FindServices(BluetoothServiceID id)
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
            return AccessStatus.Unknown;
        }
    }
}
