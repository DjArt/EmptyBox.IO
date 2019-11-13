using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices;
using Windows.Devices.Enumeration;
using Windows.Media.Devices;
using Windows.Media.Audio;

using EmptyBox.IO.Access;
using EmptyBox.IO.Media.Audio;
using AudioGraph = Windows.Media.Audio.AudioGraph;
using System.IO;
using EmptyBox.IO.Devices.Enumeration;

namespace EmptyBox.IO.Devices.Audio
{
    public sealed class AudioOutputDevice : IAudioInputDevice
    {
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public DeviceInformation InternalDeviceInformation { get; }
        public ConnectionStatus ConnectionStatus { get; private set; }
        public string Name => InternalDeviceInformation.Name;
        public IDevice Parent => throw new NotImplementedException();

        internal AudioOutputDevice(DeviceInformation info)
        {
            InternalDeviceInformation = info;
            DeviceEnumerator.ConnectionStatusChangedByID += DeviceEnumerator_ConnectionStatusChangedByID;
            DeviceEnumerator.DeviceInformationUpdated += DeviceEnumerator_DeviceInformationUpdated;
        }

        private void DeviceEnumerator_DeviceInformationUpdated(string sender, DeviceInformationUpdate args)
        {
            if (sender == InternalDeviceInformation.Id)
            {
                InternalDeviceInformation.Update(args);
            }
        }

        private void DeviceEnumerator_ConnectionStatusChangedByID(string sender, ConnectionStatus args)
        {
            if (sender == InternalDeviceInformation.Id && ConnectionStatus != args)
            {
                ConnectionStatus = args;
                ConnectionStatusChanged?.Invoke(this, ConnectionStatus);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
