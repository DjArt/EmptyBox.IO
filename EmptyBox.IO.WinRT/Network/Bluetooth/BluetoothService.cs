using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Interoperability;
using Windows.Networking.Sockets;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothService : IBluetoothService
    {
        private RfcommDeviceService _Service;
        private BluetoothServiceID _ServiceID;

        public RfcommDeviceService Service => _Service;
        public BluetoothServiceID ServiceID => _ServiceID;
        public IBluetoothDevice Device => throw new PlatformNotSupportedException();

        public BluetoothService(RfcommDeviceService service)
        {
            _Service = service;
            _ServiceID = service.ServiceId.ToBluetoothServiceID();
        }
    }
}
