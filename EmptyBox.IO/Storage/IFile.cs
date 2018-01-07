using System.IO;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IFile : IStorageItem
    {
        Task<Stream> Open();
    }
}
