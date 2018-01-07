using EmptyBox.IO.Devices.Radio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Interoperability;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public class BluetoothAdapter : IBluetoothAdapter
    {
        private Android.Bluetooth.BluetoothAdapter Adapter { get; set; }
        public bool DeviceWatcherIsActive { get; private set; }
        public bool ServiceWatcherIsActive { get; private set; }
        public RadioStatus RadioStatus => Adapter.State.ToRadioStatus();

        public event BluetoothDeviceWatcher DeviceAdded;
        public event BluetoothDeviceWatcher DeviceRemoved;
        public event BluetoothDeviceWatcher DeviceUpdated;

        private BluetoothAdapter(Android.Bluetooth.BluetoothAdapter adapter)
        {
            Adapter = adapter;
        }

        public Task<IEnumerable<IBluetoothDevice>> FindDevices()
        {
            throw new NotImplementedException();
        }

        public Task<AccessStatus> SetRadioStatus(RadioStatus state)
        {
            throw new NotImplementedException();
        }

        public Task<bool> StartDeviceWatcher()
        {
            throw new NotImplementedException();
        }

        public Task<bool> StopDeviceWatcher()
        {
            throw new NotImplementedException();
        }
    }
}