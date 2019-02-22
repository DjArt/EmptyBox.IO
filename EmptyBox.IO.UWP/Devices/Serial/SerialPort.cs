using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Serial;

namespace EmptyBox.IO.Devices.Serial
{
    public sealed class SerialPort : ISerialPort
    {
        public event DeviceConnectionStatusHandler ConnectionStatusChanged;

        public ConnectionStatus ConnectionStatus => throw new NotImplementedException();
        public string Name { get; private set; }

        public IDevice Parent => throw new NotImplementedException();

        internal SerialPort()
        {

        }
        
        public SerialSocket CreateSocket()
        {
            return new SerialSocket(null);
        }

        public void Dispose()
        {
            
        }

        ISerialSocket ISerialSocketProvider.CreateSocket()
        {
            return CreateSocket();
        }

        ISocket ISocketProvider.CreateSocket()
        {
            return CreateSocket();
        }
    }
}
