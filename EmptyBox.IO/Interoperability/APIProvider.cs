using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmptyBox.IO.Interoperability
{
    internal static class APIProvider
    {
        private static List<Assembly> Cache;

        internal static object GetTaskResult(Task obj)
        {
            return obj.GetType().GetRuntimeProperty("Result").GetValue(obj);
        }

        internal static bool IsCompatible(Assembly assembly)
        {
            try
            {
                Type type = assembly.ExportedTypes.FirstOrDefault(x => x.FullName == "EmptyBox.IO.Interoperability.Сompatibility");
                if (type != null)
                {
                    return (bool)type.GetTypeInfo().DeclaredMethods.First(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(StandardRealizationAttribute)) && x.Name == "IsCompatible").Invoke(null, new object[0]);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        internal static List<Assembly> GetСompatibleAssembly()
        {
            if (Cache == null)
            {
                try
                {
                    Assembly main = Assembly.Load(new AssemblyName("EmptyBox.IO"));
                    Cache = new List<Assembly>();
                    try
                    {
                        Assembly android = Assembly.Load(new AssemblyName("EmptyBox.IO.Android"));
                        if (IsCompatible(android)) Cache.Add(android);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Assembly ios = Assembly.Load(new AssemblyName("EmptyBox.IO.iOS"));
                        if (IsCompatible(ios)) Cache.Add(ios);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Assembly linux = Assembly.Load(new AssemblyName("EmptyBox.IO.Linux"));
                        if (IsCompatible(linux)) Cache.Add(linux);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Assembly macos = Assembly.Load(new AssemblyName("EmptyBox.IO.macOS"));
                        if (IsCompatible(macos)) Cache.Add(macos);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Assembly tizen = Assembly.Load(new AssemblyName("EmptyBox.IO.Tizen"));
                        if (IsCompatible(tizen)) Cache.Add(tizen);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Assembly uwp = Assembly.Load(new AssemblyName("EmptyBox.IO.UWP"));
                        if (IsCompatible(uwp)) Cache.Add(uwp);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Assembly windows = Assembly.Load(new AssemblyName("EmptyBox.IO.Windows"));
                        if (IsCompatible(windows)) Cache.Add(windows);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Assembly wrtd10 = Assembly.Load(new AssemblyName("EmptyBox.IO.WRTD10"));
                        if (IsCompatible(wrtd10)) Cache.Add(wrtd10);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Assembly wrtd81 = Assembly.Load(new AssemblyName("EmptyBox.IO.WRTD81"));
                        if (IsCompatible(wrtd81)) Cache.Add(wrtd81);
                    }
                    catch
                    {

                    }
                    return Cache;
                }
                catch
                {
                    throw new PlatformNotSupportedException();
                }
            }
            else
            {
                return Cache;
            }
        }
    }
}
