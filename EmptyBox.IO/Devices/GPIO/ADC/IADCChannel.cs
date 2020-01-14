using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.GPIO.ADC
{
    public interface IADCChannel : IDevice
    {
        uint ChannelNumber { get; }
        IADCController Parent { get; }

        double ReadRatio();
        int ReadValue();
    }
}
