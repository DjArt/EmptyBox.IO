using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Enumeration
{
    public interface IDeviceProvider<TDevice>
    {
        event EventHandler<TDevice> DeviceAdded;
        event EventHandler<TDevice> DeviceRemoved;

        bool IsStarted { get; }

        Task<IEnumerable<TDevice>> Find();
        void StartWatcher();
        void StopWatcher();
    }
}
