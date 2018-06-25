using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
using System.Numerics;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Sensors
{
    public interface IMagnetometer : IDevice
    {
        event MagnetometerValueChanged ValueChanged;

        Task<ValResult<Vector3, AccessStatus>> GetValue();
    }
}
