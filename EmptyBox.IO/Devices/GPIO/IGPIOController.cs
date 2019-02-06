using EmptyBox.ScriptRuntime.Results;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public interface IGPIOController : IDevice
    {
        uint PinCount { get; }

        Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> OpenPin(uint number);
        Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> OpenPin(uint number, GPIOPinSharingMode shareMode);
    }
}
