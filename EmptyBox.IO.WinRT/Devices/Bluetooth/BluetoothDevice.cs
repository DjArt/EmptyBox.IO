using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Network.Bluetooth;
using Windows.Devices.Enumeration;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothDevice : IBluetoothDevice
    {
        public string Name => throw new NotImplementedException();
        public MACAddress Address => throw new NotImplementedException();

        public BluetoothLinkType DeviceType => throw new NotImplementedException();

        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();

        public DevicePairStatus PairStatus => throw new NotImplementedException();

        public event DeviceConnectionStatusHandler ConnectionStatusEvent;

        internal BluetoothDevice(DeviceInformation device)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<BluetoothAccessPoint, byte[]>> GetSDPRecords()
        {
            throw new NotImplementedException();
        }
    }
}
