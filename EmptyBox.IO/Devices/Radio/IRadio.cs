using EmptyBox.IO.Access;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Radio
{
    public interface IRadio : IDevice
    {
        RadioStatus RadioStatus { get; }
        Task<bool> SetRadioStatus(RadioStatus state);
    }
}
