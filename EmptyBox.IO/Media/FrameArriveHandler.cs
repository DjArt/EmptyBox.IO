using EmptyBox.IO.Media.Frames;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media
{
    public delegate void FrameArriveHandler<in TFrame>(IFrameProvider<TFrame> provider, TFrame frame)
        where TFrame : IFrame;
}
