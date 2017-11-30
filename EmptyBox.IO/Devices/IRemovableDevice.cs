using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices
{
    public delegate void DeviceConnectionStatusHandler(IRemovableDevice device, DeviceStatus status);
    public interface IRemovableDevice : IDevice
    {
        event DeviceConnectionStatusHandler ConnectionStatus;
    }
}
