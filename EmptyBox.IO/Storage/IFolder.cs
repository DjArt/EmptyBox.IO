using EmptyBox.IO.Access;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IFolder : IStorageItem
    {
        Task<IEnumerable<IStorageItem>> GetItems();
        Task<IFolder> CreateFolder(string name);
        Task<IFile> CreateFile(string name);
    }
}
