using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Storage
{
    public interface IFile : IStorageItem
    {
        Task<Stream> Open();
    }
}
