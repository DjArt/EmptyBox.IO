using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices
{
    public interface IDevice
    {
        Task<DeviceStatus> GetDeviceStatus();
    }
}
