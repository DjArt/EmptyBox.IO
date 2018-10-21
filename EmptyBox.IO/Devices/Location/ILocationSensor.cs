using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
using System.Numerics;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Location
{
    public interface ILocationSensor : IDevice
    {
        event LocationChanged LocationChanged;

        Task<ValResult<Vector4, AccessStatus>> GetValue();
    }
}
