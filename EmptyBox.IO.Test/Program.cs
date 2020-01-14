using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmptyBox.IO.Devices;
using EmptyBox.IO.Devices.Enumeration;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Network.Bluetooth;
using EmptyBox.IO.Network.Bluetooth.Classic;

using static System.Console;

namespace EmptyBox.IO.Test
{
    class Program
    {
        async static Task Main(string[] args)
        {
            IDeviceEnumerator a = DeviceEnumeratorProvider.Get();
            IBluetoothAdapter bt = await a.GetDefault<IBluetoothAdapter>();
            bt.DeviceFound += Bt_DeviceFound;
            bt.DeviceLost += Bt_DeviceLost;
            await bt.ActivateSearcher();
            ReadKey();
        }

        private static void Bt_DeviceLost(IDeviceSearcher<IBluetoothDevice> provider, IBluetoothDevice device)
        {
            WriteLine($"Lost: {device.Name}");
        }

        private static async void Bt_DeviceFound(IDeviceSearcher<IBluetoothDevice> provider, IBluetoothDevice device)
        {
            WriteLine($"Found: {device.Name}");
            if (device.Mode == BluetoothMode.Classic)
            {
                IEnumerable<BluetoothClassicAccessPoint> services = await device.GetClassicServices(BluetoothSDPCacheMode.Uncached);
                foreach (BluetoothClassicAccessPoint point in services)
                {
                    WriteLine($"  {point}");
                }
            }
        }
    }
}
