using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EmptyBox.IO.Network.MAC
{
    public struct MACAddress : IAddress
    {
        public const byte Length = 6;
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
                    throw new ArgumentOutOfRangeException(string.Format("Длина MAC-адреса должна быть равна {0} байтам!", Length));
                }
            }
        }
        private byte[] _Address;

        public MACAddress(byte[] value)
        {
            if (value.Length == Length)
            {
                _Address = new byte[Length];
                Array.Copy(value, _Address, Length);
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Длина MAC-адреса должна быть равна {0} байтам!", Length));
            }
        }
    }
}