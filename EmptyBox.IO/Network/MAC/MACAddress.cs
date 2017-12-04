using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Network.MAC
{
    public struct MACAddress : IAddress
    {
        private const byte LENGTH = 6;
        private const string FORMAT = "X2";

        public static MACAddress Parse(string value)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(string value, out MACAddress address)
        {
            throw new NotImplementedException();
        }

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
        private byte[] _Address;

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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i0 = 0; i0 < _Address.Length; i0++)
            {
                result.Append(Address[0].ToString(FORMAT));
                if (i0 + 1 < _Address.Length) result.Append('.');
            }
            return result.ToString();
        }
    }
}
