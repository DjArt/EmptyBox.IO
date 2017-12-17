using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;
using EmptyBox.IO.Interoperability;
using EmptyBox.IO.Network;
using EmptyBox.IO.Serializator;

namespace EmptyBox.IO.WRTTests
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySerializer bs = new BinarySerializer(Encoding.UTF32);
            Console.ReadKey();
        }
    }
}
