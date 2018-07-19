using EmptyBox.IO.Access;
using EmptyBox.ScriptRuntime.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IMovableStorageItem : IStorageItem
    {
        Task<VoidResult<AccessStatus>> MoveTo(IFolder folder);
        Task<VoidResult<AccessStatus>> CopyTo(IFolder folder);
        Task<VoidResult<AccessStatus>> Delete();
    }
}
