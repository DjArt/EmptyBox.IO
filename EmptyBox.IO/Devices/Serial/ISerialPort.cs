using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Serial;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Serial
{
    public interface ISerialPort : IDevice, ISerialSocketProvider
    {
        
    }
}
