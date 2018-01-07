using System;
using Windows.Media.Capture;

namespace EmptyBox.IO.Devices.Camera
{
    public class Camera : ICamera
    {
        public event DeviceConnectionStatusHandler ConnectionStatus;
        public event DeviceConnectionStatusHandler ConnectionStatusEvent;

        protected MediaCapture MediaCapture;

        ConnectionStatus IRemovableDevice.ConnectionStatus => throw new NotImplementedException();

        public Camera()
        {

        }
    }
}
