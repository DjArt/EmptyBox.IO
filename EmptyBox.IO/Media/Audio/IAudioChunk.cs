using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.Audio
{
    public interface IAudioChunk
    {
        float this[uint index] { get; set; }
        uint Size { get; }
    }
}
