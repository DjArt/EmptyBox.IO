using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using EmptyBox.IO.Interoperability;
using System.Net;
using EmptyBox.IO.Network.Help;

namespace EmptyBox.IO.Network.IP
{
    public sealed class TCPConnectionListener : APointedConnectionListener<IPAddress, IPPort, ITCPConnectionProvider>
    {
        private Task? _ReceiveLoop;
        
        public Socket Socket { get; private set; }

        public TCPConnectionListener(IPAccessPoint localhost)
        {
            IsActive = false;
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(localhost.ToIPEndPoint());
        }

        public override async Task<bool> Start()
        {
            await Task.Yield();
            if (!IsActive)
            {
                Socket.Listen(512);
                IsActive = true;
                _ReceiveLoop = Task.Run(ReceiveLoop);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override async Task<bool> Stop()
        {
            await Task.Yield();
            if (IsActive)
            {
                Socket.Listen(0);
                IsActive = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void ReceiveLoop()
        {
            await Task.Yield();
            while (IsActive)
            {
                try
                {
                    Socket received = Socket.Accept();
                    TCPConnection tcpsocket = new TCPConnection(received, (received.RemoteEndPoint as IPEndPoint).ToIPAccessPoint());
                    await tcpsocket.Open();
                    OnConnectionReceive(tcpsocket);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
