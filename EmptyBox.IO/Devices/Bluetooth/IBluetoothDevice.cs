using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDevice : IRemovableDevice
    {
        string Name { get; }
        BluetoothLinkType DeviceType { get; }
    }
}
