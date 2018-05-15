using EmptyBox.IO.Access;
using EmptyBox.IO.Interoperability;
using EmptyBox.ScriptRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public static class GPIOProvider
    {
        public static async Task<RefResult<IGPIOController, AccessStatus>> GetDefault()
        {
            await Task.Yield();
            List<Assembly> libs = APIProvider.GetСompatibleAssembly();
            foreach (Assembly asm in libs)
            {
                try
                {
                    Type type = asm.ExportedTypes.FirstOrDefault(x => x.FullName == "EmptyBox.IO.Devices.GPIO.GPIOController");
                    if (type != null)
                    {
                        Task item = (Task)type.GetTypeInfo().DeclaredMethods.First(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(StandardRealizationAttribute))).Invoke(null, new object[0]);
                        dynamic result = APIProvider.GetTaskResult(item);
                        return new RefResult<IGPIOController, AccessStatus>(result.Result, result.Status, result.Exception);
                    }
                }
                catch (Exception ex)
                {
                    return new RefResult<IGPIOController, AccessStatus>(null, AccessStatus.UnknownError, ex);
                }
            }
            return new RefResult<IGPIOController, AccessStatus>(null, AccessStatus.NotSupported, new PlatformNotSupportedException());
        }
    }
}
