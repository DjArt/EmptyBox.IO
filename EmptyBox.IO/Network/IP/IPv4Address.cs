﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace EmptyBox.IO.Network.IP
{
    public struct IPv4Address : IIPAddress
    {
        public static IPv4Address Localhost { get => _Localhost; }
        private readonly static IPv4Address _Localhost = new IPv4Address(127, 0, 0, 1);

        public ushort? Port { get; set; }
        public const byte Length = 4;
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
                    throw new ArgumentOutOfRangeException(string.Format("Длина IPv4-адреса должна быть равна {0} байтам!", Length));
                }
            }
        }
        private byte[] _Address;

        public IPv4Address(params byte[] value)
        {
            if (value.Length == Length)
            {
                _Address = new byte[Length];
                Array.Copy(value, _Address, Length);
                Port = null;
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Длина IPv4-адреса должна быть равна {0} байтам!", Length));
            }
        }

        public IPv4Address(ushort port, params byte[] value)
        {
            if (value.Length == Length)
            {
                _Address = new byte[Length];
                Array.Copy(value, _Address, Length);
                Port = port;
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Длина IPv4-адреса должна быть равна {0} байтам!", Length));
            }
        }

        public IPAddress ToIPAddress()
        {
            return new IPAddress(_Address);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(Address[0]);
            result.Append('.');
            result.Append(Address[1]);
            result.Append('.');
            result.Append(Address[2]);
            result.Append('.');
            result.Append(Address[3]);
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
