using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media
{
    public interface ICodec
    {
        CodecComputableClass ComputableClass { get; }
        bool CanEncode { get; }
        bool CanDecode { get; }
    }
}
