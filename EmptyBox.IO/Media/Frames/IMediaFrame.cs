using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.Frames
{
    public interface IMediaFrame : IFrame
    {
        TimeSpan? RelativeTime { get; }
        TimeSpan? RelativeSystemTime { get; }
    }
}
