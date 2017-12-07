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
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothDevice : IBluetoothDevice
    {
        public BluetoothLinkType DeviceType { get; protected set; }
        public event DeviceConnectionStatusHandler ConnectionStatusEvent;

        private Windows.Devices.Bluetooth.BluetoothDevice _Device;
        public Windows.Devices.Bluetooth.BluetoothDevice Device => _Device;
        public string Name => _Device.Name;
        public ConnectionStatus ConnectionStatus => _Device.ConnectionStatus.ToConnectionStatus();

        public MACAddress Address => throw new NotImplementedException();

        public DevicePairStatus PairStatus => throw new NotImplementedException();

        public BluetoothDevice(Windows.Devices.Bluetooth.BluetoothDevice device)
        {
            _Device = device;
        }

        public Task<IDictionary<BluetoothAccessPoint, byte[]>> GetSDPRecords()
        {
            throw new NotImplementedException();
        }
    }
}
