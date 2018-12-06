using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.Serial
{
    public enum SerialPinChange : byte
    {
        BreakSignal = 0,
        CarrierDetected = 1,
        ClearToSend = 2,
        DataSetReady = 3,
        RingIndicator = 4
    }
}
