using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Network.Bluetooth;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothAdapter : IRadio
    {
        Task<IEnumerable<IBluetoothDevice>> FindDevices();
        Task<IEnumerable<IBluetoothService>> FindServices(BluetoothServiceID id);
    }
}
