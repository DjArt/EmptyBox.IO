using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IFolder : IMovableStorageItem
    {
        Task<RefResult<IEnumerable<IStorageItem>, AccessStatus>> GetItems();
        Task<RefResult<IFolder, AccessStatus>> CreateFolder(string name);
        Task<RefResult<IFile, AccessStatus>> CreateFile(string name);
    }
}
