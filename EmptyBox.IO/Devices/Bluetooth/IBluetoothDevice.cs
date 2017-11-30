using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothDevice : IRemovableDevice
    {
        BluetoothLinkType DeviceType { get; }
    }
}
