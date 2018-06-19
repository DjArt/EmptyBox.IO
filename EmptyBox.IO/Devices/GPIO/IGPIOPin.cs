using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public interface IGPIOPin : IDevice
    {
        event GPIOPinEvent ValueChanged;
        
        TimeSpan DebounceTime { get; set; }
        uint PinNumber { get; }
        IEnumerable<GPIOPinMode> SupportedModes { get; }
        GPIOPinSharingMode SharingMode { get; }
        GPIOPinSharingMode OpenMode { get; }

        Task<ValResult<bool, AccessStatus>> SetValue(GPIOPinValue value);
        Task<ValResult<GPIOPinValue, AccessStatus>> GetValue();
        Task<ValResult<bool, AccessStatus>> SetMode(GPIOPinMode mode);
        Task<ValResult<GPIOPinMode, AccessStatus>> GetMode();
        Task<ValResult<GPIOPinEdge, AccessStatus>> SupportedEventModes();
    }
}
