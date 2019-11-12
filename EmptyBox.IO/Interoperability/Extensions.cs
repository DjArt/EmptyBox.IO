using EmptyBox.IO.Network;
using EmptyBox.IO.Network.IP;
using System.Net;

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
            return new IPAccessPoint(new Network.IP.IPAddress(endPoint.Address.GetAddressBytes()), new IPPort((ushort)endPoint.Port));
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
