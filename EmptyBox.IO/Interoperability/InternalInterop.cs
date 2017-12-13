using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace EmptyBox.IO.Interoperability
{
    internal static class InternalInterop
    {
        internal static Assembly GetAssembly()
        {
            try
            {
                Assembly core = Assembly.Load(new AssemblyName("EmptyBox.IO"));
                try
                {
                    Assembly asm = Assembly.Load(new AssemblyName("EmptyBox.IO.UWP"));
                    return asm;
                }
                catch
                {

                }
                try
                {
                    Assembly asm = Assembly.Load(new AssemblyName("EmptyBox.IO.WRT"));
                    return asm;
                }
                catch
                {

                }
                try
                {
                    Assembly asm = Assembly.Load(new AssemblyName("EmptyBox.IO.Windows"));
                    return asm;
                }
                catch
                {

                }
                try
                {
                    Assembly asm = Assembly.Load(new AssemblyName("EmptyBox.IO.Android"));
                    return asm;
                }
                catch
                {

                }
                try
                {
                    Assembly asm = Assembly.Load(new AssemblyName("EmptyBox.IO.Tizen"));
                    return asm;
                }
                catch
                {

                }
                try
                {
                    Assembly asm = Assembly.Load(new AssemblyName("EmptyBox.IO.iOS"));
                    return asm;
                }
                catch
                {

                }
                throw new Exception("Can't find any platform-specific assembly.");
            }
            catch
            {
                throw new Exception("Can't load core assembly. Why?");
            }
        }
    }
}
