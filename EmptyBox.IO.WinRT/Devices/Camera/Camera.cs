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
        protected MediaCapture MediaCapture;

        public Camera()
        {

        }

        public Task<DeviceStatus> GetDeviceStatus()
        {
            throw new NotImplementedException();
        }
    }
}
