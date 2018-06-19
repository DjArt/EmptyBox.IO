namespace EmptyBox.IO.Devices.Sensors
{
    public delegate void AccelerometerPositionChanged(IAccelerometer device, (double X, double Y, double Z) position);
}
