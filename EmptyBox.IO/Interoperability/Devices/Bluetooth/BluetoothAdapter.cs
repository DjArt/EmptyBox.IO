using EmptyBox.IO.Devices.Bluetooth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBox.IO.Interoperability.Devices.Bluetooth
{
    public static class BluetoothAdapter
    {
        private static object GetTaskResult(Task obj)
        {
            return obj.GetType().GetRuntimeProperty("Result").GetValue(obj);
        }

        public static async Task<IBluetoothAdapter> GetDefaultBluetoothAdapter()
        {
            await Task.Yield();
            Assembly asm = InternalInterop.GetAssembly();
            if (asm != null)
            {
                Type type = asm.ExportedTypes.FirstOrDefault(x => x.FullName == "EmptyBox.IO.Devices.Bluetooth.BluetoothAdapter");
                if (type != null)
                {
                    Task item = (Task)type.GetTypeInfo().DeclaredMethods.First(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(StandardRealizationAttribute))).Invoke(null, new object[0]);
                    return (IBluetoothAdapter)GetTaskResult(item);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
