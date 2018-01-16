using EmptyBox.IO.Interoperability;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public static class BluetoothAdapterProvider
    {
        public static async Task<IBluetoothAdapter> GetDefault()
        {
            await Task.Yield();
            Assembly asm = InternalInterop.GetAssembly();
            if (asm != null)
            {
                Type type = asm.ExportedTypes.FirstOrDefault(x => x.FullName == "EmptyBox.IO.Devices.Bluetooth.BluetoothAdapter");
                if (type != null)
                {
                    Task item = (Task)type.GetTypeInfo().DeclaredMethods.First(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(StandardRealizationAttribute))).Invoke(null, new object[0]);
                    return (IBluetoothAdapter)InternalInterop.GetTaskResult(item);
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
