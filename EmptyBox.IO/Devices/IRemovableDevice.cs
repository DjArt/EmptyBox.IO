namespace EmptyBox.IO.Devices
{
    public delegate void DeviceConnectionStatusHandler(IRemovableDevice device, ConnectionStatus status);
    public interface IRemovableDevice : IDevice
    {
        event DeviceConnectionStatusHandler ConnectionStatusEvent;
        ConnectionStatus ConnectionStatus { get; }
    }
}
