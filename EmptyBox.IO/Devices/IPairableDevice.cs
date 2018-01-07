namespace EmptyBox.IO.Devices
{
    public interface IPairableDevice : IRemovableDevice
    {
        DevicePairStatus PairStatus { get; }
    }
}
