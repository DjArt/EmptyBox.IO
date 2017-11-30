using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Bluetooth
{
    public interface IBluetoothService
    {
        BluetoothServiceID ServiceID { get; }
        IBluetoothDevice Device { get; }
    }
}
