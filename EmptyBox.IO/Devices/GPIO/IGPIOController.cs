using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public interface IGPIOController : IDevice
    {
        uint PinCount { get; }

        Task<IGPIOPin> OpenPin(uint number);
        Task<IGPIOPin> OpenPin(uint number, GPIOPinSharingMode shareMode);
    }
}
