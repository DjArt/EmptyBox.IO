using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EmptyBox.IO.Network.IP
{
    public struct IPv6Address : IIPAddress
    {
        public const byte Length = 16;
        public byte[] Address
        {
            get => _Address;
            set
            {
                if (value.Length == Length)
                {
                    _Address = new byte[Length];
                    Array.Copy(value, _Address, Length);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(string.Format("Длина IPv6-адреса должна быть равна {0} байтам!", Length));
                }
            }
        }
        private byte[] _Address;
        public ushort? Port { get; set; }

        public IPv6Address(params byte[] value)
        {
            if (value.Length == Length)
            {
                _Address = new byte[Length];
                Array.Copy(value, _Address, Length);
                Port = null;
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Длина IPv6-адреса должна быть равна {0} байтам!", Length));
            }
        }

        public IPAddress ToIPAddress()
        {
            return new IPAddress(_Address);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i0 = 0; i0 < 16; i0++)
            {
                result.Append(Address[0]);
                if (i0 < 15)
                {
                    result.Append('.');
                }
            }
            if (Port.HasValue)
            {
                result.Append(':');
                result.Append(Port);
            }
            return result.ToString();
        }

        public IPEndPoint ToEndPoint()
        {
            return new IPEndPoint(ToIPAddress(), Port.Value);
        }
    }
}
