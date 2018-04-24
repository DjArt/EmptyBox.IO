using System;

namespace EmptyBox.IO.Devices
{
    public interface IDevice : IDisposable
    {
        event DeviceConnectionStatusHandler ConnectionStatusChanged;

        ConnectionStatus ConnectionStatus { get; }
        string Name { get; }
    }
}
