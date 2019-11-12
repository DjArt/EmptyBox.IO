using EmptyBox.IO.Access;
using EmptyBox.IO.Storage.FileSystems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Storage
{
    public interface IDrive : IDevice
    {
        ulong Space { get; }
        DriveType Type { get; }

        Task<IEnumerable<IFileSystem>> GetFileSystems();
    }
}
