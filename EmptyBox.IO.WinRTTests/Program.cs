using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.MAC;

namespace EmptyBox.IO.WinRTTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TY();
            Console.ReadKey();
        }

        static async void TY()
        {
            BluetoothAdapter bt = await BluetoothAdapter.GetDefaultBluetoothAdapter();
            BluetoothConnectionSocketHandler listener = new BluetoothConnectionSocketHandler(new BluetoothAccessPoint(new MACAddress(), new BluetoothPort(0x4444)));
            await listener.Start();
            var x = await bt.FindDevices();
            var i = x.Select(o => o.GetServices().Result);
            Console.WriteLine("Finded devices:");
            foreach (var j in x)
            {
                Console.WriteLine(j.Name);
            }
            Console.WriteLine("Finded services:");
            foreach (var j in i)
            {
                foreach (var e in j)
                {
                    Console.WriteLine(e);
                }
            }
            BluetoothConnectionSocket z = new BluetoothConnectionSocket(new BluetoothAccessPoint(new MACAddress(0x48, 0x50, 0x73, 0xE9, 0x5C, 0x20), new BluetoothPort(0x1105)));
            var k = await z.Open();
            Console.WriteLine(k);
            //IEnumerable<BluetoothAccessPoint> services = await bt.FindServices(new BluetoothPort(0x4444));
            //services.All(x => { Console.WriteLine(x); return true; });
        }

        private static void Bt_ServiceAdded(IBluetoothAdapter connection, BluetoothAccessPoint device)
        {
            Console.WriteLine(device);
        }
    }
}
