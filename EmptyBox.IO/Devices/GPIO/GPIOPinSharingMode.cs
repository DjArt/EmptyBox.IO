using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.GPIO
{
    public enum GPIOPinSharingMode : byte
    {
        Exclusive = 0,
        ReadOnlySharedAccess = 1,
    }
}
