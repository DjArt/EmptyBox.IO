using EmptyBox.IO.Media.Encoding;
using EmptyBox.IO.Media.Frames;
using EmptyBox.IO.Media.OpenCV.Interoperability;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Media.OpenCV
{
    public sealed class FileVideoWriter : IFrameConsumer<IVideoFrame>, IDisposable
    {
        private VideoWriter? InternalVideoWriter;
        private bool IsDisposed;

        public VideoEncodingProperties EncodingProperties { get; }
        public Uri FilePath { get; }

        public FileVideoWriter(Uri filePath, VideoEncodingProperties encodingProperties)
        {
            EncodingProperties = encodingProperties;
            FilePath = filePath;
        }

        private void StopActivity()
        {
            InternalVideoWriter?.Release();
            InternalVideoWriter?.Dispose();
            InternalVideoWriter = null;
        }

        public void ConsumeFrame(IFrameProvider<IVideoFrame> provider, IVideoFrame frame)
        {
            if (IsDisposed) throw new ObjectDisposedException(string.Empty);
            if (InternalVideoWriter == null) throw new InvalidOperationException($"{nameof(FileVideoWriter)} must be started before perform this action.");
            Mat matFrame = frame.ToMat();
            bool requireResize = frame.EncodingProperties.Resolution != EncodingProperties.Resolution;
            if (requireResize)
            {
                matFrame = matFrame.Resize(EncodingProperties.Resolution.ToOcvSize());
            }
            InternalVideoWriter.Write(matFrame);
            if (requireResize)
            {
                matFrame.Dispose();
            }
        }

        public void Dispose()
        {
            StopActivity();
            IsDisposed = true;
        }

        public async Task Start()
        {
            if (IsDisposed) throw new ObjectDisposedException(string.Empty);
            await Task.Yield();
            InternalVideoWriter = new VideoWriter(FilePath.AbsoluteUri, FourCC.Default, EncodingProperties.FPS, EncodingProperties.Resolution.ToOcvSize());
        }

        public async Task Stop()
        {
            if (IsDisposed) throw new ObjectDisposedException(string.Empty);
            await Task.Yield();
            StopActivity();
        }
    }
}
