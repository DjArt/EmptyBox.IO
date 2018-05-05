using EmptyBox.ScriptRuntime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public interface IGPIO : IDevice
    {
        Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> OpenPin(uint number);
        Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> OpenPin(uint number, GPIOPinSharingMode shareMode);
    }
}
