using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.ApplicationModel;
using Windows.System.Display;
using Windows.Graphics.Display;

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
