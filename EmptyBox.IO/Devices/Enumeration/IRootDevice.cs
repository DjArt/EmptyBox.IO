using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Enumeration
{
    public interface IRootDevice : IDevice
    {
        IDevice HardwareBus { get; }
        IDevice VirtualBus { get; }

        Task<TDevice> GetDefault<TDevice>()
            where TDevice : IDevice;
        Task<IEnumerable<TDevice>> GetAll<TDevice>()
            where TDevice : IDevice;
        Task<IDevice> GetFromPath(string path);
    }
}
