using System;
using System.Text;
using EmptyBox.IO.Serializator;
using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.WRTTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadKey();
        }

        static async void Test()
        {
            BluetoothAdapter a = await BluetoothAdapter.GetDefaultBluetoothAdapter();
            a.DeviceProvider.DeviceAdded += DeviceProvider_DeviceAdded;
            a.DeviceProvider.StartWatcher();
        }

        private static void DeviceProvider_DeviceAdded(object sender, IBluetoothDevice e)
        {
            Console.WriteLine(e.Name + " " + e.Address);
        }
    }
}
