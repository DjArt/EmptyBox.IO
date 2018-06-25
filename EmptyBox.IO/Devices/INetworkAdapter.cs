using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices
{
    public interface INetworkAdapter : IDevice
    {
        IANAInterfaceType IANAInterfaceType { get; }
    }
}
