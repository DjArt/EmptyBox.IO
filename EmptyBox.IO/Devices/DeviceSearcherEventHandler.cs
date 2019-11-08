namespace EmptyBox.IO.Devices
{
    public delegate void DeviceSearcherEventHandler<in TDevice>(IDeviceSearcher<TDevice> provider, TDevice device) where TDevice : IDevice;
}
