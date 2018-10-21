using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media
{
    public struct ContainerStream
    {
        public string Name { get; }
        public IFormat Format { get; }
        public IEnumerable<byte> Data { get; }
    }
}
