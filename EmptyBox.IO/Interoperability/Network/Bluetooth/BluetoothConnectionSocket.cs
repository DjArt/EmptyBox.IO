using EmptyBox.IO.Network;
using EmptyBox.IO.Network.Bluetooth;
using System;
using System.Linq;
using System.Reflection;

namespace EmptyBox.IO.Interoperability.Network.Bluetooth
{
    public static class BluetoothConnectionSocket
    {
        public static IConnection GetBluetoothConnectionSocket(BluetoothAccessPoint accessPoint)
        {
            Assembly asm = InternalInterop.GetAssembly();
            if (asm != null)
            {
                Type type = asm.ExportedTypes.FirstOrDefault(x => x.FullName == "EmptyBox.IO.Network.Bluetooth.BluetoothConnectionSocket");
                if (type != null)
                {
                    return (IConnection)type.GetTypeInfo().DeclaredConstructors.First(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(StandardRealizationAttribute))).Invoke(new object[] { accessPoint });
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
