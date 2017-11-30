using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace EmptyBox.IO.Network.IP
{
    public class TCPConnectionSocketHandler : IConnectionSocketHandler
    {
        protected ILinkLevelAddress _LocalHost;
        public ILinkLevelAddress LocalHost
        {
            get => _LocalHost;
            set
            {
                if (!IsActive)
                {
                    _LocalHost = value;
                }
            }
        }
        public bool IsActive { get; protected set; }
        public event ConnectionReceivedDelegate ConnectionSocketReceived;
        public Socket Socket { get; protected set; }

        public TCPConnectionSocketHandler(IIPAddress localhost)
        {
            _LocalHost = localhost;
            IsActive = false;
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(localhost.ToEndPoint());
        }

        public async Task<bool> Start()
        {
            await Task.Yield();
            IsActive = true;
            try
            {
                Socket.Listen(512);
                ReceiveLoop();
                return true;
            }
            catch
            {
                IsActive = false;
                return false;
            }
        }

        public async Task<bool> Stop()
        {
            await Task.Yield();
            Socket.Listen(0);
            IsActive = false;
            return false;
        }

        protected async void ReceiveLoop()
        {
            await Task.Yield();
            while (IsActive)
            {
                try
                {
                    Socket received = Socket.Accept();
                    TCPConnectionSocket tcpsocket = new TCPConnectionSocket(received, new IPv4Address(), new IPv4Address());
                    await tcpsocket.Open();
                    ConnectionSocketReceived?.Invoke(this, tcpsocket);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
