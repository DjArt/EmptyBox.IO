using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices
{
    public interface IDeviceProvider<TDevice> where TDevice : IDevice
    {
        event EventHandler<TDevice> DeviceAdded;
        event EventHandler<TDevice> DeviceRemoved;

        bool IsStarted { get; }

        Task<IEnumerable<TDevice>> Find();
        void StartWatcher();
        void StopWatcher();
    }
}
