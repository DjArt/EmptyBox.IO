using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network
{
    public interface IAccessPoint
    {
        IAddress Address { get; }
        IPort Port { get; }
    }
}
