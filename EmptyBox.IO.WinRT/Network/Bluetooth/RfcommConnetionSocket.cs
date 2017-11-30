using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class RfcommConnetionSocket : IConnectionSocket
    {
        public ILinkLevelAddress RemoteHost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsActive => throw new NotImplementedException();

        public int ReadBufferLength => throw new NotImplementedException();

        public int WriteBufferLength => throw new NotImplementedException();

        public ILinkLevelAddress LocalHost => throw new NotImplementedException();

        public event ConnectionInterruptHandler ConnectionInterrupt;
        public event MessageReceiveHandler MessageReceived;

        public Task<bool> Close()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Open()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Send(byte[] data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Send(ILinkLevelAddress host, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
