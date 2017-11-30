using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace EmptyBox.IO.Network.IP
{
    public class TCPConnectionSocket : IConnectionSocket
    {
        protected ILinkLevelAddress _RemoteHost;
        protected ILinkLevelAddress _LocalHost;

        public Socket Socket { get; protected set; }
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
        public ILinkLevelAddress RemoteHost
        {
            get => _RemoteHost;
            set
            {
                if (!IsActive)
                {
                    _RemoteHost = value;
                }
            }
        }
        public int ReadBufferLength => Socket.ReceiveBufferSize;
        public int WriteBufferLength => Socket.SendBufferSize;
        public bool IsActive { get; protected set; }
        public event ConnectionInterruptHandler ConnectionInterrupt;
        public event MessageReceiveHandler MessageReceived;

        internal TCPConnectionSocket(Socket socket, IIPAddress remotehost, IIPAddress localhost)
        {
            Socket = socket;
            _RemoteHost = remotehost;
            _LocalHost = localhost;
            IsActive = false;
        }

        public TCPConnectionSocket(IIPAddress remotehost)
        {
            _RemoteHost = remotehost;
            IsActive = false;
            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        protected async void ReceiveLoop()
        {
            await Task.Yield();
            byte[] buffer = new byte[ReadBufferLength];
            int count = -1;
            while (IsActive)
            {
                try
                {
                    count = Socket.Receive(buffer);
                    if (count > 0)
                    {
                        byte[] newbuffer = new byte[count];
                        Array.Copy(buffer, newbuffer, count);
                        MessageReceived?.Invoke(this, _RemoteHost, newbuffer);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2147467259)
                    {
                        await Close();
                    }
                    //FOR DEBUG
                    else
                    {
                        throw ex;
                    }
                }
            }
        }

        public async Task<bool> Close()
        {
            await Task.Yield();
            ConnectionInterrupt?.Invoke(this);
            IsActive = false;
            Socket.Shutdown(SocketShutdown.Both);
            return true;
        }

        public async Task<bool> Open()
        {
            await Task.Yield();
            if (!Socket.Connected)
            {
                Socket.Connect((_RemoteHost as IIPAddress).ToEndPoint());
            }
            if (Socket.Connected)
            {
                IsActive = true;
                ReceiveLoop();
                return true;
            }
            return false;
        }

        public async Task<bool> Send(byte[] data)
        {
            await Task.Yield();
            int count = Socket.Send(data);
            if (count == data.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Send(ILinkLevelAddress host, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
