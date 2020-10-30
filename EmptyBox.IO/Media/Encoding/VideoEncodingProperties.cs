using EmptyBox.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Media.Encoding
{
    public sealed class VideoEncodingProperties
    {
        public Size Resolution { get; }
        public double FPS { get; }
        public TimeSpan FrameDuration { get; }

        public VideoEncodingProperties(Size resolution)
        {
            Resolution = resolution;
            FrameDuration = TimeSpan.MaxValue;
        }

        public VideoEncodingProperties(Size resolution, double fps)
        {
            Resolution = resolution;
            FPS = fps;
            FrameDuration = TimeSpan.FromSeconds(Math.Pow(fps, -1));
        }

        public VideoEncodingProperties(Size resolution, TimeSpan frameDuration)
        {
            Resolution = resolution;
            FPS = Math.Pow(frameDuration.TotalSeconds, -1);
            FrameDuration = frameDuration;
        }
    }
}
