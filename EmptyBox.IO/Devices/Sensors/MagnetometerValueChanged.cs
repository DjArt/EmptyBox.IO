using System.Numerics;

namespace EmptyBox.IO.Devices.Sensors
{
    public delegate void MagnetometerValueChanged(IOrientationSensor device, Vector3 value);
}
