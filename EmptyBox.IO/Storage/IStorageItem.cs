using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Storage
{
    public interface IStorageItem
    {
        IStorageItem Path { get; }
        string Name { get; }
    }
}
