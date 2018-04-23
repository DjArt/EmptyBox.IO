using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.GPIO
{
    public interface IGPIOPin : IDevice
    {
        uint Number { get; }
    }
}
