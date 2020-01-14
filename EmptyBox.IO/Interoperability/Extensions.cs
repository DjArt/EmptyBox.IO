using EmptyBox.IO.Network;
using EmptyBox.IO.Network.IP;
using System.Net;
using System;
using System.Net.Sockets;

namespace EmptyBox.IO.Interoperability
{
    public static class Extensions
    {
        public static IPEndPoint ToIPEndPoint(this IAccessPoint<Network.IP.IPAddress, IPPort> accessPoint)
        {
            return new IPEndPoint(new System.Net.IPAddress(accessPoint.Address.Address), accessPoint.Port.Value);
        }

        public static IPAccessPoint ToIPAccessPoint(this IPEndPoint endPoint)
        {
            int size = 0;
            switch (endPoint.AddressFamily)
            {
                case AddressFamily.InterNetwork:
                    size = Network.IP.IPAddress.Length4;
                    break;
                case AddressFamily.InterNetworkV6:
                    size = Network.IP.IPAddress.Length6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            byte[] address = new byte[size];
            Array.Copy(endPoint.Address.GetAddressBytes(), 0, address, 0, size);
            return new IPAccessPoint(new Network.IP.IPAddress(address), new IPPort((ushort)endPoint.Port));
        }

        public static uint? ToUInt(this int? value)
        {
            if (value.HasValue)
            {
                return (uint)value.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
