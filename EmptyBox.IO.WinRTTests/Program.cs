using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.Bluetooth;

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
            await bt.StartListener(new BluetoothPort(0x4444), new byte[0]);
            IEnumerable<BluetoothAccessPoint> services = await bt.FindServices(new BluetoothPort(0x4444));
            services.All(x => { Console.WriteLine(x); return true; });
        }
    }
}
