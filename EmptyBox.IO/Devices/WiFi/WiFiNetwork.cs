using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.WiFi
{
    public sealed class WiFiNetwork : IDevice
    {
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public string Name { get; private set; }

        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
