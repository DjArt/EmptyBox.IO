using EmptyBox.Collections.Generic;
using EmptyBox.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace EmptyBox.IO.Devices
{
    public interface IDevice : ITreeNode<IDevice>, IDisposable
    {
        event DeviceConnectionStatusHandler ConnectionStatusChanged;

        ConnectionStatus ConnectionStatus { get; }
        string Name { get; }
    }
}
