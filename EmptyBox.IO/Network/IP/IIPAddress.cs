using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EmptyBox.IO.Network.IP
{
    public interface IIPAddress : ILinkLevelAddress
    {
        ushort? Port { get; set; }
        IPAddress ToIPAddress();
        IPEndPoint ToEndPoint();
    }
}
