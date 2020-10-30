using EmptyBox.IO.Media.Frames;
using EmptyBox.IO.Media.OpenCV.Frames;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.OpenCV.Interoperability
{
    public static class FrameExtensions
    {
        public static Mat ToMat(this IVideoFrame frame)
        {
            if (frame is VideoFrame ocvFrame)
            {
                return ocvFrame.NativeFrame;
            }
            else
            {
                Mat result;
                unsafe
                {
                    result = new Mat((int)frame.EncodingProperties.Resolution.Height, (int)frame.EncodingProperties.Resolution.Width, default, new IntPtr(frame.GetBuffer()));
                }
                return result;
            }
        }
    }
}
