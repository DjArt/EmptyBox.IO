using System;
using System.Linq;
using System.Text;

namespace EmptyBox.IO.Network.IP
{
    public sealed class IPAddress : IAddress
    {
        public const byte Length4 = 4;
        public const byte Length6 = 16;

        public byte[] Address { get; private set; }

        public IPAddress(params byte[] value)
        {
            if (value.Length == Length4 || value.Length == Length6)
            {
                Address = new byte[value.Length];
                Array.Copy(value, Address, value.Length);
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Длина IP-адреса должна быть равна {0} или {1} байтам.", Length4, Length6));
            }
        }

        public static bool operator ==(IPAddress x, IPAddress y)
        {
            return x.Address.SequenceEqual(y.Address);
        }

        public static bool operator !=(IPAddress x, IPAddress y)
        {
            return !(x == y);
        }

        public static implicit operator IPAddress(System.Net.IPAddress address)
        {
            if (address == null)
            {
                return null;
            }
            else
            {
                return new IPAddress(address.GetAddressBytes());
            }
        }

        public static IPAddress Parse(string value)
        {
            TryParse(value, out IPAddress result);
            return result;
        }

        public static bool TryParse(string value, out IPAddress address)
        {
            bool result = System.Net.IPAddress.TryParse(value, out System.Net.IPAddress tmp);
            if (result)
            {
                address = new IPAddress(tmp.GetAddressBytes());
            }
            else
            {
                address = null;
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is IPAddress address)
            {
                return this == address;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i0 = 0; i0 < Address.Length; i0++)
            {
                result.Append(Address[i0]);
                if (i0 + 1 < Address.Length) result.Append('.');
            }
            return result.ToString();
        }

        public bool Equals(IAddress other)
        {
            if (other is IPAddress address)
            {
                return this == address;
            }
            else
            {
                return false;
            }
        }
    }
}
