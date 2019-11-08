using EmptyBox.Collections.Generic;
using EmptyBox.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace EmptyBox.IO.Devices
{
    public interface IDevice : IObservableTreeNode<IDevice>, IDisposable
    {
        event DeviceConnectionStatusHandler ConnectionStatusChanged;

        ConnectionStatus ConnectionStatus { get; }
        string Name { get; }
        string Path { get; }
    }
}
