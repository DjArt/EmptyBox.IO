using System;
using Windows.Media.Capture;

namespace EmptyBox.IO.Devices.Camera
{
    public sealed class Camera : ICamera
    {
        public event DeviceConnectionStatusHandler ConnectionStatus;
        public event DeviceConnectionStatusHandler ConnectionStatusEvent;

        public string Name { get; private set; }

        protected MediaCapture MediaCapture;

        ConnectionStatus IRemovableDevice.ConnectionStatus => throw new NotImplementedException();

        public Camera()
        {

        }
    }
}
