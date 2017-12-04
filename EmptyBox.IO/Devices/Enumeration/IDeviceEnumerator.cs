using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices
{
    public interface IDeviceEnumerator
    {
        Task<IBluetoothDevice> TryGetDevice(BluetoothAddress address);
        Task<T> TryGetDevice<T>(Predicate<IDevice> selector);
        Task<IEnumerable<T>> TryGetDevices<T>(Predicate<IDevice> selector);
    }
}
