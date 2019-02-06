using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO.PWM
{
    public interface IPWMController : IDevice
    {
        double MaxFrequency { get; }
        double MinFrequency { get; }
        double Frequency { get; set; }
        uint PinCount { get; }

        Task<RefResult<IPWMPin, GPIOPinOpenStatus>> OpenPin(uint number);
    }
}
