using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public interface IGPIOPin : IDevice
    {
        event GPIOPinValueChanged ValueChanged;
        
        TimeSpan DebounceTime { get; set; }
        bool EventChecking { get; set; }
        uint PinNumber { get; }
        IEnumerable<GPIOPinMode> SupportedModes { get; }
        GPIOPinSharingMode SharingMode { get; }
        GPIOPinSharingMode OpenMode { get; }
        IGPIOController Controller { get; }

        void SetValue(GPIOPinValue value);
        GPIOPinValue GetValue();
        void SetMode(GPIOPinMode mode);
        GPIOPinMode GetMode();
    }
}
