using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Devices.Bluetooth;
using EmptyBox.IO.Devices.Enumeration;

namespace EmptyBox.IO.Test.Devices.Bluetooth
{
    public class GetBluetoothAdapters : ITest
    {
        public string Description => "Получение Bluetooth-адаптеров, установленных в системе.";

        public event EventHandler<string> Log;

        public async Task<string> Run()
        {
            StringBuilder result = new StringBuilder();
            IDeviceEnumerator enumerator = DeviceEnumeratorProvider.Get();
            IBluetoothAdapter @default = await enumerator.GetDefault<IBluetoothAdapter>();
            result.AppendFormat("Найден Bluetooth-адаптер по умолчанию: {0}({1})", @default?.Name, @default?.Address);
            return result.ToString();
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
