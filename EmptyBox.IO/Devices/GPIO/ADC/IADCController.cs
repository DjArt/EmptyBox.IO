using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO.ADC
{
    public interface IADCController : IDevice
    {
        int MaxValue { get; }
        int MinValue { get; }
        uint Resolution { get; }
        uint ChannelCount { get; }

        IADCChannel OpenChannel(uint number);
    }
}
