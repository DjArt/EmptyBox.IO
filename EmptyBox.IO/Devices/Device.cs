using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices
{
    public sealed class Device : IDevice
    {
        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
