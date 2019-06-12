using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
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

        Task<VoidResult<AccessStatus>> Rename(string name);
        Task<VoidResult<AccessStatus>> MoveTo(IFolder folder);
        Task<VoidResult<AccessStatus>> CopyTo(IFolder folder);
        Task<VoidResult<AccessStatus>> Delete();
    }
}
