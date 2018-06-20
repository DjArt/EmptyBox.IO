using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;
using System.Numerics;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Sensors
{
    public interface IAccelerometer : IDevice
    {
        event AccelerometerValueChanged PositionChanged;

        Task<ValResult<Vector3, AccessStatus>> GetValue();
    }
}
