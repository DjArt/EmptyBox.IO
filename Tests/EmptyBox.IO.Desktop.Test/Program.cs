using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;
using EmptyBox.IO.Test.Devices.Bluetooth;
using EmptyBox.IO.Test;

namespace EmptyBox.IO.Desktop.Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            Test();
            ReadKey();
        }

        public static async void Test()
        {
            ITest test = new GetPairedBluetoothDevices();
            WriteLine(await test.Run());
        }
    }
}
