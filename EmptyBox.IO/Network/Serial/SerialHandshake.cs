﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Serial
{
    [Flags]
    public enum SerialHandshake : byte
    {
        None = 0,
        RequestToSend = 1,
        XOnXOff = 2
    }
}
