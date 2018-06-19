using EmptyBox.ScriptRuntime;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public interface IGPIOController : IDevice
    {
        Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> OpenPin(uint number);
        Task<RefResult<IGPIOPin, GPIOPinOpenStatus>> OpenPin(uint number, GPIOPinSharingMode shareMode);
    }
}
