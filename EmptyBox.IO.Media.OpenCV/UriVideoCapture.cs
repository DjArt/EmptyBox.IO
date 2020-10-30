using EmptyBox.IO.Media.Encoding;
using EmptyBox.IO.Media.OpenCV.Frames;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Size = EmptyBox.Foundation.Size;

namespace EmptyBox.IO.Media.OpenCV
{
    public class UriVideoCapture : IFrameProvider<VideoFrame>, IDisposable
    {
        private bool IsDisposed;
        private VideoCapture? InternalFrameProvider;
        private Task? ReceiveLoopTask;
        private CancellationTokenSource? TokenSource;

        public event FrameArriveHandler<VideoFrame>? FrameArrived;

        public bool IsActive => (!TokenSource?.IsCancellationRequested) ?? false;
        public DateTime? StartTime { get; private set; }
        public Uri RecordingUri { get; }
        public VideoEncodingProperties? EncodingProperties { get; private set; }

        public UriVideoCapture(Uri recordingUri)
        {
            RecordingUri = recordingUri;
        }

        private void StopActivity()
        {
            TokenSource?.Cancel();
            TokenSource?.Dispose();
            TokenSource = null;
            EncodingProperties = null;
            ReceiveLoopTask?.Dispose();
            ReceiveLoopTask = null;
            InternalFrameProvider?.Dispose();
            InternalFrameProvider = null;
            StartTime = null;
        }

        private void ReceiveLoop(object _token)
        {
            CancellationToken token = (CancellationToken)_token;
            while (!token.IsCancellationRequested)
            {
                if (InternalFrameProvider!.Grab())
                {
                    Mat matFrame = InternalFrameProvider.RetrieveMat();
                    if (matFrame.Empty())
                    {
                        StopActivity();
                        break;
                    }
                    VideoFrame frame = new VideoFrame(matFrame, EncodingProperties!)
                    {
                        RelativeTime = StartTime - DateTime.Now,
                    };
                    FrameArrived?.Invoke(this, frame);
                }
            }
        }

        public async Task Start()
        {
            if (IsDisposed) throw new ObjectDisposedException(string.Empty);
            await Task.Yield();
            TokenSource = new CancellationTokenSource();
            InternalFrameProvider = new VideoCapture(RecordingUri.AbsoluteUri);
            EncodingProperties = new VideoEncodingProperties(new Size(InternalFrameProvider.FrameWidth, InternalFrameProvider.FrameHeight), InternalFrameProvider.Fps);
            TokenSource = new CancellationTokenSource();
            ReceiveLoopTask = new Task(ReceiveLoop, TokenSource.Token, TokenSource.Token, TaskCreationOptions.LongRunning);
            ReceiveLoopTask.Start();
        }

        public async Task Stop()
        {
            if (IsDisposed) throw new ObjectDisposedException(string.Empty);
            await Task.Yield();
            StopActivity();
        }

        public void Dispose()
        {
            StopActivity();
            IsDisposed = true;
        }
    }
}
