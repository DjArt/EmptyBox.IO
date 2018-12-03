using System;

namespace EmptyBox.IO.Network.Bluetooth
{
    public class BluetoothPort : IPort
    {
        public Guid ID { get; private set; }

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

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
