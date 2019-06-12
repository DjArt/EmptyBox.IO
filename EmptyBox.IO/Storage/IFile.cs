using System.IO;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IFile : IStorageItem
    {
        ulong Size { get; }

        Task<Stream> Open();
    }
}
