using System;
using System.Text;
using System.Linq;

namespace EmptyBox.IO.Network
{
    public class MACAddress : IAddress, IEquatable<MACAddress>
    {
        private const byte LENGTH = 6;
        private const string FORMAT = "X2";

        public static MACAddress Parse(string value)
        {
            TryParse(value, out MACAddress result);
            return result;
        }

        public static bool TryParse(string value, out MACAddress address)
        {
            //For testing and debbuging easy algorithm. Change after testing
            string cleared = value.Replace("(", "").Replace(")", "").ToUpper();
            string[] bytes = cleared.Split(':', '-');
            if (bytes.Length == 6)
            {
                byte[] tmp = new byte[6];
                for (int i0 = 0; i0 < 6; i0++)
                {
                    tmp[i0] = byte.Parse(bytes[i0], System.Globalization.NumberStyles.HexNumber);
                }
                address = new MACAddress(tmp);
                return true;
            }
            else
            {
                address = new MACAddress();
                return false;
            }
        }

        public static bool operator ==(MACAddress a0, MACAddress a1)
        {
            return a0._Address.SequenceEqual(a1._Address);
        }

        public static bool operator !=(MACAddress a0, MACAddress a1)
        {
            return !(a0 == a1);
        }

        public static implicit operator byte[](MACAddress address)
        {
            return address.Address;
        }

        private byte[] _Address;

        public byte[] Address
        {
            get => _Address;
            set
            {
                if (value.Length == LENGTH)
                {
                    _Address = new byte[value.Length];
                    Array.Copy(value, _Address, value.Length);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(string.Format("Длина MAC-адреса должна быть равна {0} байтам.", LENGTH));
                }
            }
        }

        public MACAddress(params byte[] value)
        {
            if (value.Length == LENGTH)
            {
                _Address = new byte[value.Length];
                Array.Copy(value, _Address, value.Length);
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Длина MAC-адреса должна быть равна {0} байтам.", LENGTH));
            }
        }

        public MACAddress(ulong address)
        {
            _Address = BitConverter.GetBytes(address);
            Array.Resize(ref _Address, 6);
            Array.Reverse(_Address);
        }

        public override bool Equals(object obj)
        {
            if (obj is MACAddress mac)
            {
                return this == mac;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return _Address.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (_Address != null)
            {
                for (int i0 = 0; i0 < _Address.Length; i0++)
                {
                    result.Append(Address[i0].ToString(FORMAT));
                    if (i0 + 1 < _Address.Length) result.Append(':');
                }
            }
            else
            {
                result.Append("Unspecified address");
            }
            return result.ToString();
        }

        public bool Equals(MACAddress other)
        {
            return this == other;
        }
    }
}
