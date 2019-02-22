using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.Power
{
    public enum BatteryTechnology : ushort
    {
        Unknown = 0,
        Pb = 1,
        NiCd = 2,
        NiFe = 3,
        NiMH = 4,
        NiZc = 5,
        SiZc = 6,
        SiCd = 7,
        NiH = 8,
        LiIon = 9,
        LiPol = 10
    }
}
