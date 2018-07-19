using EmptyBox.IO.Storage.FileSystems;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Storage
{
    public static class StorageItemExtensions
    {
        public static IFileSystem GetFileSystem(this IStorageItem item)
        {
            switch (item)
            {
                case null:
                    return null;
                case IFileSystem fileSystem:
                    return fileSystem;
                default:
                    return item.Container.GetFileSystem();
            }
        }
    }
}
