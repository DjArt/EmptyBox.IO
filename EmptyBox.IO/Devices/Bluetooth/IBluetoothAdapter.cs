using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Radio;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public interface IBluetoothAdapter : IRadio
    {
        bool DeviceWatcherIsActive { get; }
        bool ServiceWatcherIsActive { get; }
        IReadOnlyDictionary<BluetoothPort, byte[]> ActiveListeners { get; }
        event BluetoothDeviceFindedHandler DeviceFinded;
        event BluetoothServiceFindedHandler ServiceFinded;
        Task<IEnumerable<IBluetoothDevice>> FindDevices();
        Task<IEnumerable<BluetoothAccessPoint>> FindServices(BluetoothPort id);
        Task<bool> StartDeviceWatcher();
        Task<bool> StopDeviceWatcher();
        Task<bool> StartServiceWatcher();
        Task<bool> StopServiceWatcher();
    }
}
