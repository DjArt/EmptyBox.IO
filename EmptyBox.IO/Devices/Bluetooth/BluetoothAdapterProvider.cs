using EmptyBox.IO.Access;
using EmptyBox.IO.Interoperability;
using EmptyBox.ScriptRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.Bluetooth
{
    public static class BluetoothAdapterProvider
    {
        public static async Task<RefResult<IBluetoothAdapter, AccessStatus>> GetDefault()
        {
            await Task.Yield();
            List<Assembly> libs = APIProvider.GetСompatibleAssembly();
            foreach (Assembly asm in libs)
            {
                try
                {
                    Type type = asm.ExportedTypes.FirstOrDefault(x => x.FullName == "EmptyBox.IO.Devices.Bluetooth.BluetoothAdapter");
                    if (type != null)
                    {
                        Task item = (Task)type.GetTypeInfo().DeclaredMethods.First(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(StandardRealizationAttribute))).Invoke(null, new object[0]);
                        return new RefResult<IBluetoothAdapter, AccessStatus>((IBluetoothAdapter)APIProvider.GetTaskResult(item), AccessStatus.Success, null);
                    }
                }
                catch (Exception ex)
                {
                    return new RefResult<IBluetoothAdapter, AccessStatus>(null, AccessStatus.UnknownError, ex);
                }
            }
            return new RefResult<IBluetoothAdapter, AccessStatus>(null, AccessStatus.NotSupported, null);
        }
    }
}
