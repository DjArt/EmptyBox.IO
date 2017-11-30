using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices;
using Windows.Media.Capture;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothDevice : IBluetoothDevice
    {
        public BluetoothLinkType DeviceType { get; protected set; }
        public event DeviceConnectionStatusHandler ConnectionStatus;

        public Task<DeviceStatus> GetDeviceStatus()
        {
            throw new NotImplementedException();
        }
    }
}
