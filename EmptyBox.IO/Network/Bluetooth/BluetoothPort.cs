using System;

namespace EmptyBox.IO.Network.Bluetooth
{
    public sealed class BluetoothPort : IPort
    {
        private static readonly byte[] SHORT_SUFFIX_BYTE = { 0, 0, 0, 16, 128, 0, 0, 128, 95, 155, 52, 251 };
        private static readonly ushort[] SHORT_SUFFIX_USHORT = { 0, 0x1000, 128, 0, 0, 128, 95, 155, 52, 251 };

        public Guid ID { get; private set; }
        public uint? ShortID
        {
            get
            {
                byte[] id = ID.ToByteArray();
                for (byte i0 = 0; i0 < 12; i0++)
                {
                    if (id[i0 + 4] != SHORT_SUFFIX_BYTE[i0])
                    {
                        return null;
                    }
                }
                return BitConverter.ToUInt32(id, 0);
            }
        }

        public BluetoothPort(Guid id)
        {
            ID = id;
        }

        public BluetoothPort(uint shortid)
        {
            ID = new Guid(shortid, 0, 0x1000, 128, 0, 0, 128, 95, 155, 52, 251);
        }

        public static implicit operator Guid(BluetoothPort port)
        {
            return port.ID;
        }

        public static implicit operator byte[](BluetoothPort port)
        {
            return port.ID.ToByteArray();
        }

        public static bool operator ==(BluetoothPort x, BluetoothPort y)
        {
            return x.ID == y.ID;
        }

        public static bool operator !=(BluetoothPort x, BluetoothPort y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (obj is BluetoothPort port)
            {
                return this == port;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        public bool Equals(IPort other)
        {
            if (other is BluetoothPort port)
            {
                return this == port;
            }
            else
            {
                return false;
            }
        }
    }
}
