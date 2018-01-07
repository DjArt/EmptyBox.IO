using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IFolder : IStorageItem
    {
        Task<IEnumerable<IStorageItem>> GetItems();
    }
}
