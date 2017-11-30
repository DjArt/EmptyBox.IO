using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public delegate void ConnectionReceivedDelegate(IConnectionSocketHandler handler, IConnectionSocket socket);
    public interface IConnectionSocketHandler
    {
        ILinkLevelAddress LocalHost { get; set; }
        bool IsActive { get; }
        event ConnectionReceivedDelegate ConnectionSocketReceived;
        Task<bool> Start();
        Task<bool> Stop();
    }
}
