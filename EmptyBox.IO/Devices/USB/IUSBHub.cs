using EmptyBox.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.USB
{
    public interface IUSBHub : IDevice, IObservableTreeNode<IUSBDevice>
    {
        byte PortCount { get; }
    }
}
