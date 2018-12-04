using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Serial
{
    public interface ISerialSocketProvider : ISocketProvider
    {
        new ISerialSocket CreateSocket();
    }
}
