using EmptyBox.IO.Access;
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
        new IGPIOController Parent { get; }

        void SetValue(GPIOPinValue value);
        GPIOPinValue GetValue();
        void SetMode(GPIOPinMode mode);
        GPIOPinMode GetMode();
    }
}
