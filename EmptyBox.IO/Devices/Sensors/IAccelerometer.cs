using EmptyBox.IO.Access;
using System.Numerics;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Sensors
{
    public interface IAccelerometer : IDevice
    {
        event AccelerometerValueChanged PositionChanged;

        Task<Vector3> GetValue();
    }
}
