using System.Numerics;

namespace EmptyBox.IO.Devices.Sensors
{
    public delegate void AccelerometerValueChanged(IAccelerometer device, Vector3 value);
}
