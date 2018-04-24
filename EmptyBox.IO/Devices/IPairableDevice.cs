namespace EmptyBox.IO.Devices
{
    public interface IPairableDevice : IDevice
    {
        DevicePairStatus PairStatus { get; }
    }
}
