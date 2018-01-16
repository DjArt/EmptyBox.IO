using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.Test.Devices.Bluetooth
{
    public class GetBluetoothAdapters : ITest
    {
        public string Description => "Получение Bluetooth-адаптеров, установленных в системе.";

        public async Task<string> Run()
        {
            StringBuilder result = new StringBuilder();
            IBluetoothAdapter @default = await BluetoothAdapterProvider.GetDefault();
            if (@default != null)
            {
                result.AppendFormat("Найден Bluetooth-адаптер по умолчанию: {0}({1})", @default.Name, @default.Address);
            }
            else
            {
                result.Append("Bluetooth-адаптера по умолчанию не было найдено.");
            }
            return result.ToString();
        }
    }
}
