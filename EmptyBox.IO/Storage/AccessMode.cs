using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Storage
{
    [Flags]
    public enum AccessMode
    {
        Read = 0b01,
        Write = 0b10,
    }
}
