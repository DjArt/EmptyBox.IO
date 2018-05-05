using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Radio
{
    public interface IRadio : IDevice
    {
        RadioStatus RadioStatus { get; }
        Task<VoidResult<AccessStatus>> SetRadioStatus(RadioStatus state);
    }
}
