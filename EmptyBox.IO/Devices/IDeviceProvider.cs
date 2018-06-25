using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices
{
    public interface IDeviceProvider<out TDevice> where TDevice : IDevice
    {
        event DeviceProviderEventHandler<TDevice> DeviceFound;
        event DeviceProviderEventHandler<TDevice> DeviceLost;

        bool IsStarted { get; }

        IAsyncCovariantResult<IEnumerable<TDevice>> Find();
        Task<VoidResult<AccessStatus>> StartWatcher();
        Task<VoidResult<AccessStatus>> StopWatcher();
    }
}
