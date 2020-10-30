using EmptyBox.IO.Media.Encoding;
using EmptyBox.IO.Media.Frames;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Media
{
    public interface IFrameConsumer<in TFrame>
        where TFrame : IFrame
    {
        VideoEncodingProperties EncodingProperties { get; }

        void ConsumeFrame(IFrameProvider<TFrame> provider, TFrame frame);
        Task Start();
        Task Stop();
    }
}
