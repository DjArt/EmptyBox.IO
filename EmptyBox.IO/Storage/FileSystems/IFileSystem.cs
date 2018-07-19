using EmptyBox.IO.Access;
using EmptyBox.IO.Devices.Storage;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage.FileSystems
{
    public interface IFileSystem : IStorageItem
    {
        ulong Space { get; }
        IDrive Drive { get; }

        Task<RefResult<IFolder, AccessStatus>> GetRoot();
    }
}
