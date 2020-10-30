using System;
using System.Text;
using System.Linq;
using System.Globalization;

namespace EmptyBox.IO.Network
{
    public sealed class MACAddress : IAddress
    {
        private const byte LENGTH = 6;
        private const string FORMAT = "X2";

        public byte[] Address { get; private set; }

        public MACAddress(params byte[] value)
        {
            if (value.Length == LENGTH)
            {
                Address = new byte[value.Length];
                Array.Copy(value, Address, value.Length);
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Длина MAC-адреса должна быть равна {0} байтам.", LENGTH));
            }
        }

        public MACAddress(ulong address)
        {
            byte[] _address = BitConverter.GetBytes(address);
            Array.Resize(ref _address, 6);
            Array.Reverse(_address);
            Address = _address;
        }

        public static bool operator ==(MACAddress a0, MACAddress a1)
        {
            return a0.Address.SequenceEqual(a1.Address);
        }

        public static bool operator !=(MACAddress a0, MACAddress a1)
        {
            return !(a0 == a1);
        }

        public static implicit operator byte[] (MACAddress address)
        {
            return address.Address;
        }

        public static MACAddress? Parse(string value)
        {
            TryParse(value, out MACAddress? result);
            return result;
        }

        public static bool TryParse(string value, out MACAddress? address)
        {
            //For testing and debbuging easy algorithm. Change after testing
            string cleared = value.Replace("(", "").Replace(")", "").ToUpper();
            string[] bytes = cleared.Split(':', '-');
            if (bytes.Length == 6)
            {
                byte[] tmp = new byte[6];
                for (int i0 = 0; i0 < 6; i0++)
                {
                    tmp[i0] = byte.Parse(bytes[i0], NumberStyles.HexNumber);
                }
                address = new MACAddress(tmp);
                return true;
            }
            else
            {
                address = null;
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is MACAddress address)
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
            if (Address != null)
            {
                for (int i0 = 0; i0 < Address.Length; i0++)
                {
                    result.Append(Address[i0].ToString(FORMAT));
                    if (i0 + 1 < Address.Length) result.Append(':');
                }
            }
            else
            {
                result.Append("Unspecified address");
            }
            return result.ToString();
        }

        public bool Equals(IAddress other)
        {
            if (other is MACAddress address)
            {
                return this == address;
            }
            else
            {
                return false;
            };
        }
    }
}
