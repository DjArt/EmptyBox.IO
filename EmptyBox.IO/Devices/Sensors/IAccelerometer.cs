using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Sensors
{
    public interface IAccelerometer : IDevice
    {
        event AccelerometerPositionChanged PositionChanged;

        Task<ValResult<(double X, double Y, double Z), AccessStatus>> GetPosition();
    }
}
