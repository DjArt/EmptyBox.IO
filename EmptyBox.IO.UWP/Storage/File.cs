using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using Windows.Storage;

namespace EmptyBox.IO.Storage
{
    public sealed class File : IFile
    {
        public IStorageItem Container { get; private set; }

        public string Name { get; private set; }

        public ulong Size { get; private set; }

        public DateTime DateCreated => throw new NotImplementedException();

        public DateTime DateModified => throw new NotImplementedException();

        public Task<bool> CopyTo(IFolder folder)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<bool> MoveTo(IFolder folder)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> Open()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Rename(string name)
        {
            throw new NotImplementedException();
        }
    }
}
