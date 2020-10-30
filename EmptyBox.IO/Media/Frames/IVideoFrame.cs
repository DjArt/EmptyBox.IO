using EmptyBox.IO.Media.Encoding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace EmptyBox.IO.Media.Frames
{
    public interface IVideoFrame : IMediaFrame
    {
        VideoEncodingProperties EncodingProperties { get; }
    }
}
