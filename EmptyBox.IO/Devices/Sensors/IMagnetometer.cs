using EmptyBox.IO.Access;
using System.Numerics;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Sensors
{
    public interface IMagnetometer : IDevice
    {
        event MagnetometerValueChanged ValueChanged;

        Task<Vector3> GetValue();
    }
}
