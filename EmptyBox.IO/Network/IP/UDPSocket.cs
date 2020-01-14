using EmptyBox.IO.Network.Help;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Network.IP
{
    class UDPSocket : APointedSocket<IPAddress, IPPort, IUDPSocketProvider>, IUDPSocket
    {
        public override Task<bool> Close()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Open()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Send(IAccessPoint<IAddress, IPort> receiver, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
