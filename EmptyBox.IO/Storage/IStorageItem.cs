using EmptyBox.IO.Access;
using System;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IStorageItem
    {
        IStorageItem Container { get; }
        string Name { get; }
        DateTime DateCreated { get; }
        DateTime DateModified { get; }

        Task<bool> Rename(string name);
        Task<bool> MoveTo(IFolder folder);
        Task<bool> CopyTo(IFolder folder);
        Task<bool> Delete();
    }
}
