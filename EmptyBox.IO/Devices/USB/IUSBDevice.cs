using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.USB
{
    public interface IUSBDevice : IDevice
    {
        byte PortNumber { get; }
    }
}
