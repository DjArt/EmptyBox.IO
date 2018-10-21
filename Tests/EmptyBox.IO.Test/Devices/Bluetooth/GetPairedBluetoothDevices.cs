using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Test.Devices.Bluetooth
{
    public class GetPairedBluetoothDevices : ITest
    {
        public string Description => "Получает список сопряжённых Bluetooth-устройств для адаптера по умолчанию.";

        public event EventHandler<string> Log;

        public async Task<string> Run()
        {
            StringBuilder result = new StringBuilder();
            IBluetoothAdapter @default = (await BluetoothAdapterProvider.GetDefault()).Result;
            if (@default != null)
            {
                result.AppendFormat("Найден Bluetooth-адаптер по умолчанию: {0}({1})", @default.Name, @default.DeviceProvider.Address);
                result.AppendLine();
                IEnumerable<IBluetoothDevice> devices = await @default.DeviceProvider.Find();
                foreach (IBluetoothDevice device in devices)
                {
                    result.AppendFormat("Найдено Bluetooth-устроство: {0}({1})", device.Name, device.Address);
                    result.AppendLine();
                }
            }
            else
            {
                result.Append("Bluetooth-адаптера по умолчанию не было найдено.");
            }
            return result.ToString();
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
