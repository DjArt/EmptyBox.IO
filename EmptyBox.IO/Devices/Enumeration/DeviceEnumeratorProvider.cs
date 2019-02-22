using EmptyBox.IO.Interoperability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EmptyBox.IO.Devices.Enumeration
{
    public static class DeviceEnumeratorProvider
    {
        public static IDeviceEnumerator Get()
        {
            List<Assembly> libs = APIProvider.GetСompatibleAssembly();
            foreach (Assembly asm in libs)
            {
                try
                {
                    Type type = asm.ExportedTypes.FirstOrDefault(x => x.FullName == "EmptyBox.IO.Devices.Enumeration.DeviceEnumerator");
                    return (IDeviceEnumerator)type.GetTypeInfo().DeclaredConstructors.ElementAt(0).Invoke(new object[0]);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
