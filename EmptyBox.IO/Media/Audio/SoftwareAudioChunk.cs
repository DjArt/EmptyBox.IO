using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.Audio
{
    public sealed class SoftwareAudioChunk : IAudioChunk
    {
        public float this[uint index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public uint Size => throw new NotImplementedException();
    }
}
