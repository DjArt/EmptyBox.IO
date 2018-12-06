using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Serial
{
    public enum SerialParity : byte
    {
        None = 0,
        Even = 1,
        Odd = 2,
        Mark = 3,
        Space = 4
    }
}
