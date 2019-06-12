using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.Audio
{
    public delegate void NewChunk(IAudioInput source, IAudioChunk chunk);
    public interface IAudioInput : IDisposable
    {

    }
}
