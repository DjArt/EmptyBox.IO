using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDevice : IPairableDevice
    {
        string Name { get; }
        BluetoothAddress Address { get; }
        BluetoothLinkType DeviceType { get; }
        Task<IDictionary<BluetoothAccessPoint, byte[]>> GetSDPRecords();
    }
}
