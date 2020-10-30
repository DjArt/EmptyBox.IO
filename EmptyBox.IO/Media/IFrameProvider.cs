using EmptyBox.IO.Media.Encoding;
using EmptyBox.IO.Media.Frames;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Media
{
    public interface IFrameProvider<out TFrame>
        where TFrame : IFrame
    {
        event FrameArriveHandler<TFrame> FrameArrived;

        bool IsActive { get; }
        DateTime? StartTime { get; }
        VideoEncodingProperties? EncodingProperties { get; }

        Task Start();
        Task Stop();
    }
}
