using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.IO.Access;
using EmptyBox.IO.Devices.Bluetooth;

namespace EmptyBox.IO.Test.Devices.Bluetooth
{
    public class GetBluetoothAdapters : ITest
    {
        public string Description => "Получение Bluetooth-адаптеров, установленных в системе.";

        public event EventHandler<string> Log;

        public async Task<string> Run()
        {
            StringBuilder result = new StringBuilder();
            var res = await BluetoothAdapterProvider.GetDefault();
            switch (res.Status)
            {
                case AccessStatus.Success:
                    IBluetoothAdapter @default = res.Result;
                    result.AppendFormat("Найден Bluetooth-адаптер по умолчанию: {0}({1})", @default.Name, @default.DeviceProvider.Address);
                    break;
                case AccessStatus.UnknownError:
                    result.AppendFormat("Ошибка: {0}", res.Exception);
                    break;
                default:
                    result.AppendFormat("Ошибка {0}: {1}", res.Status, res.Exception);
                    break;
            }
            return result.ToString();
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
