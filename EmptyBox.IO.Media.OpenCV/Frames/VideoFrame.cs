using EmptyBox.IO.Media.Encoding;
using EmptyBox.IO.Media.Frames;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using Size = EmptyBox.Foundation.Size;

namespace EmptyBox.IO.Media.OpenCV.Frames
{
    public sealed class VideoFrame : IVideoFrame
    {
        public VideoEncodingProperties EncodingProperties { get; }
        public TimeSpan? RelativeTime { get; set;  }
        public TimeSpan? RelativeSystemTime { get; set; }
        public Mat NativeFrame { get; }

        public VideoFrame(Mat nativeFrame)
        {
            NativeFrame = nativeFrame;
            var frameSize = NativeFrame.Size();
            EncodingProperties = new VideoEncodingProperties(new Size(frameSize.Width, frameSize.Height));
        }

        public VideoFrame(Mat nativeFrame, VideoEncodingProperties encodingProperties)
        {
            NativeFrame = nativeFrame;
            EncodingProperties = encodingProperties;
        }

        public void Dispose()
        {
            NativeFrame.Dispose();
        }

        public unsafe byte* GetBuffer()
        {
            return NativeFrame.DataPointer;
        }
    }
}
