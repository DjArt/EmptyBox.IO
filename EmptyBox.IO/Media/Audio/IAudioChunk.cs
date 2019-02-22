using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.Audio
{
    public interface IAudioChunk : IDisposable
    {
        float this[uint index] { get; set; }
        uint Size { get; }
        unsafe byte* GetRawBuffer();
        byte[] GetBuffer();
    }
}
