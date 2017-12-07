using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using EmptyBox.IO.Interoperability;
using System.Net;

namespace EmptyBox.IO.Network.IP
{
    public class TCPConnectionSocketHandler : IConnectionSocketHandler
    {
        IAccessPoint IConnectionSocketHandler.LocalHost => LocalHost;

        public IPAccessPoint LocalHost { get; private set; }
        public bool IsActive { get; protected set; }
        public event ConnectionReceivedDelegate ConnectionSocketReceived;
        public Socket Socket { get; protected set; }

        public TCPConnectionSocketHandler(IPAccessPoint localhost)
        {
            LocalHost = localhost;
            IsActive = false;
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(localhost.ToIPEndPoint());
        }

        public async Task<SocketOperationStatus> Start()
        {
            await Task.Yield();
            if (!IsActive)
            {
                try
                {
                    Socket.Listen(512);
                    IsActive = true;
                    ReceiveLoop();
                    return SocketOperationStatus.Success;
                }
                catch (Exception ex)
                {
                    return SocketOperationStatus.UnknownError;
                }
            }
            else
            {
                return SocketOperationStatus.ListenerIsAlreadyStarted;
            }
        }

        public async Task<SocketOperationStatus> Stop()
        {
            await Task.Yield();
            if (IsActive)
            {
                try
                {
                    Socket.Listen(0);
                    IsActive = false;
                    return SocketOperationStatus.Success;
                }
                catch (Exception ex)
                {
                    return SocketOperationStatus.UnknownError;
                }
            }
            else
            {
                return SocketOperationStatus.ListenerIsAlreadyClosed;
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
                    TCPConnectionSocket tcpsocket = new TCPConnectionSocket(received, (received.RemoteEndPoint as IPEndPoint).ToIPAccessPoint(), LocalHost);
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
