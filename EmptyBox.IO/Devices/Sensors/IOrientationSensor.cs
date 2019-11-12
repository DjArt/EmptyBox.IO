using EmptyBox.IO.Access;
using System.Numerics;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Sensors
{
    public interface IOrientationSensor : IDevice
    {
        event OrientationChanged OrientationChanged;

        Task<Vector4> GetValue();
    }
}
