using System;
using System.Collections.Generic;
using System.Text;
using EmptyBox.IO.Network.IP;
using System.Net;

namespace EmptyBox.IO.Interoperability
{
    public static class Extensions
    {
        public static IPEndPoint ToIPEndPoint(this IPAccessPoint accessPoint)
        {
            return new IPEndPoint(new System.Net.IPAddress(accessPoint.Address.Address), accessPoint.Port.Value);
        }

        public static IPAccessPoint ToIPAccessPoint(this IPEndPoint endPoint)
        {
            return new IPAccessPoint(new Network.IP.IPAddress(endPoint.Address.GetAddressBytes()), new IPPort((ushort)endPoint.Port));
        }
    }
}
