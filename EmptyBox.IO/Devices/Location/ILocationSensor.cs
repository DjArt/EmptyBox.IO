using EmptyBox.IO.Access;
using System.Numerics;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Location
{
    public interface ILocationSensor : IDevice
    {
        event LocationChanged LocationChanged;

        Task<Vector4> GetValue();
    }
}
