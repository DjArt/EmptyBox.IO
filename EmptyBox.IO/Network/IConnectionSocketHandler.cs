using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network
{
    public interface IConnectionSocketHandler
    {
        IAccessPoint LocalHost { get; }
        bool IsActive { get; }
        event ConnectionReceivedDelegate ConnectionSocketReceived;
        Task<SocketOperationStatus> Start();
        Task<SocketOperationStatus> Stop();
    }
}
