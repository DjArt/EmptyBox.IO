using EmptyBox.IO.Access;
using EmptyBox.IO.Storage.FileSystems;
using EmptyBox.ScriptRuntime.Results;
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

        Task<RefResult<IEnumerable<IFileSystem>, AccessStatus>> GetFileSystems();
    }
}
