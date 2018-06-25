namespace EmptyBox.IO.Devices
{
    public delegate void DeviceProviderEventHandler<in TDevice>(IDeviceProvider<TDevice> provider, TDevice device) where TDevice : IDevice;
}
