using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices
{
    public enum AccessStatus
    {
        Allowed,
        DeniedBySystem,
        DeniedByUser,
        Unknown
    }
}
