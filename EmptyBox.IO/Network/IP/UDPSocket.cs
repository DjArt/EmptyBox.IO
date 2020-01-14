using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network.Help;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.IP
{
    public class UDPSocket : APointedSocket<IPAddress, IPPort, IUDPSocketProvider>, IUDPSocket
    {
        private Task? _ReceiveLoopTask;

        public Socket Socket { get; private set; }

        public UDPSocket(IPPort port)
        {
            LocalPoint = new IPAccessPoint(new IPAddress(0,0,0,0), port);
            IsActive = false;
            Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        }

        public UDPSocket(IPAccessPoint localPoint)
        {
            LocalPoint = localPoint;
            IsActive = false;
            Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        }

        private async void ReceiveLoop()
        {
            await Task.Yield();
            byte[] buffer = new byte[4096];
            while (IsActive)
            {
                try
                {
                    EndPoint sender = new IPEndPoint(0,0);
                    int count = Socket.ReceiveFrom(buffer, ref sender);
                    if (count > 0)
                    {
                        byte[] newbuffer = new byte[count];
                        Array.Copy(buffer, newbuffer, count);
                        OnMessageReceive((sender as IPEndPoint)?.ToIPAccessPoint(), newbuffer);
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

        public override async Task<bool> Close()
        {
            await Task.Yield();
            if (IsActive)
            {
                IsActive = false;
                _ReceiveLoopTask?.Wait(100);
                Socket.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override async Task<bool> Open()
        {
            await Task.Yield();
            if (!IsActive)
            {
                Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                Socket.Bind(LocalPoint.ToIPEndPoint());
                IsActive = true;
                _ReceiveLoopTask = Task.Run(ReceiveLoop);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override async Task<bool> Send(IAccessPoint<IAddress, IPort> receiver, byte[] data)
        {
            await Task.Yield();
            if (IsActive)
            {
                int count = Socket.SendTo(data, (receiver as IPAccessPoint)?.ToIPEndPoint());
                if (count == data.Length)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
