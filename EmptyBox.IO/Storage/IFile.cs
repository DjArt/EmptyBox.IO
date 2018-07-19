using System.IO;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IFile : IMovableStorageItem
    {
        ulong Size { get; }

        Task<Stream> Open();
    }
}
