using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Enumeration;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Serial;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;

namespace EmptyBox.IO.Devices.Serial
{
    public sealed class SerialPort : ISerialPort
    {
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public ConnectionStatus ConnectionStatus { get; private set; }
        public string Name => InternalDeviceInformation.Name;

        public IDevice Parent => throw new NotImplementedException();
        public DeviceInformation InternalDeviceInformation { get; }

        internal SerialPort(DeviceInformation info)
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

        ISerialSocket ISerialSocketProvider.CreateSocket()
        {
            return CreateSocket();
        }

        ISocket ISocketProvider.CreateSocket()
        {
            return CreateSocket();
        }

        public SerialSocket CreateSocket()
        {
            return new SerialSocket(this, SerialDevice.FromIdAsync(InternalDeviceInformation.Id).GetResults());
        }

        public void Dispose()
        {
            
        }
    }
}
