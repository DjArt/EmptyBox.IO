using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.IP
{
    public struct IPPort : IPort
    {
        public ushort Value { get; set; }

        public IPPort(ushort value)
        {
            Value = value;
        }
    }
}
