using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Radio;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothAdapter : IRadio
    {
        Task<List<IBluetoothDevice>> FindDevices();
    }
}
