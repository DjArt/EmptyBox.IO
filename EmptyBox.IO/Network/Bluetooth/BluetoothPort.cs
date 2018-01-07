using System;

namespace EmptyBox.IO.Network.Bluetooth
{
    public struct BluetoothPort : IPort
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

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
