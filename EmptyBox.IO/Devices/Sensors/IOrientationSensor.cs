using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;
using System.Numerics;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Sensors
{
    public interface IOrientationSensor : IDevice
    {
        event OrientationChanged OrientationChanged;

        Task<ValResult<Vector4, AccessStatus>> GetValue();
    }
}
