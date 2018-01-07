using System;
using System.Text;

namespace EmptyBox.IO.Network.IP
{
    public struct IPAddress : IAddress
    {
        public const byte Length4 = 4;
        public const byte Length6 = 16;

        public static IPAddress Parse(string value)
        {
            var tmp = System.Net.IPAddress.Parse(value);
            return new IPAddress(tmp.GetAddressBytes());
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
                address = new IPAddress();
            }
            return result;
        }

        public byte[] Address
        {
            get => _Address;
            set
            {
                if (value.Length == Length4 || value.Length == Length6)
                {
                    _Address = new byte[value.Length];
                    Array.Copy(value, _Address, value.Length);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(string.Format("Длина IP-адреса должна быть равна {0} или {1} байтам.", Length4, Length6));
                }
            }
        }
        private byte[] _Address;

        public IPAddress(params byte[] value)
        {
            if (value.Length == Length4 || value.Length == Length6)
            {
                _Address = new byte[value.Length];
                Array.Copy(value, _Address, value.Length);
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Длина IP-адреса должна быть равна {0} или {1} байтам.", Length4, Length6));
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i0 = 0; i0 < _Address.Length; i0++)
            {
                result.Append(Address[0]);
                if (i0 + 1 < _Address.Length) result.Append('.');
            }
            return result.ToString();
        }
    }
}
