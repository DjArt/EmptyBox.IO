using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using EmptyBox.IO.Interoperability;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothService : IBluetoothService
    {
        private RfcommDeviceService _Service;
        private BluetoothDevice _Device;
        private BluetoothServiceID _ServiceID;

        IBluetoothDevice IBluetoothService.Device => _Device;

        public RfcommDeviceService Service => _Service;
        public BluetoothDevice Device => _Device;
        public BluetoothServiceID ServiceID => _ServiceID;

        public BluetoothService(RfcommDeviceService service)
        {
            _Service = service;
            _Device = new BluetoothDevice(service.Device);
            _ServiceID = service.ServiceId.ToBluetoothServiceID();
        }
    }
}
