using EmptyBox.IO.Interoperability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmptyBox.IO.Devices.GPIO
{
    public static class GPIOProvider
    {
        public static async Task<IGPIOController> GetDefault()
        {
            await Task.Yield();
            List<Assembly> libs = APIProvider.GetСompatibleAssembly();
            foreach (Assembly asm in libs)
            {
                try
                {
                    Type type = asm.ExportedTypes.FirstOrDefault(x => x.FullName == "EmptyBox.IO.Devices.GPIO.GPIO");
                    if (type != null)
                    {
                        Task item = (Task)type.GetTypeInfo().DeclaredMethods.First(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(StandardRealizationAttribute))).Invoke(null, new object[0]);
                        return (IGPIOController)APIProvider.GetTaskResult(item);
                    }
                }
                catch
                {

                }
            }
            return null;
        }
    }
}
