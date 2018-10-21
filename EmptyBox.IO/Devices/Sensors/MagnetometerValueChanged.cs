using System.Numerics;

namespace EmptyBox.IO.Devices.Sensors
{
    public delegate void MagnetometerValueChanged(IMagnetometer device, Vector3 value);
}
