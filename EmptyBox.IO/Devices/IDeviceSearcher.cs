using EmptyBox.Collections.ObjectModel;
using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices
{
    public interface IDeviceSearcher<out TDevice> : IObservableTreeNode<TDevice>
        where TDevice : IDevice
    {
        event DeviceSearcherEventHandler<TDevice> DeviceFound;
        event DeviceSearcherEventHandler<TDevice> DeviceLost;

        bool SearcherIsActive { get; }

        IAsyncCovariantResult<IEnumerable<TDevice>> Search();
        Task ActivateSearcher();
        Task DeactivateSearcher();
    }
}
