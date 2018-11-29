using EmptyBox.IO.Serializator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.ScriptRuntime.Extensions;
using System.Reflection;
using System.Runtime.InteropServices;
using EmptyBox.ScriptRuntime.Extensions;
using System.Security.Principal;
using System.Security.Policy;
using System.Globalization;
using System.Collections;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Channels;

namespace EmptyBox.IO.BinarySerializer_Tests
{
    [TestClass]
    public class EachType
    {
        BinarySerializer Serializer = new BinarySerializer(Encoding.UTF32);

        [TestMethod]
        public void Test()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(int));
            uint count = 0;
            foreach (Type type in assembly.DefinedTypes)
            {
                if (type == typeof(AppDomainSetup) || type == typeof(ApplicationTrust) || type == typeof(DateTimeFormatInfo) ||
                    type == typeof(NumberFormatInfo) || type.Name == "IntSizedArray" ||
                    type.GetTypeInfo().ImplementedInterfaces.Any(x => x == typeof(IEnumerable)) && type.GetTypeInfo().ImplementedInterfaces.Any(x => x != typeof(IEnumerable<>)) ||
                    type == typeof(SoapTypeAttribute) || type == typeof(SoapMethodAttribute) || type == typeof(SoapPositiveInteger) ||
                    type == typeof(SoapNegativeInteger) || type.Name == "CrossAppDomainChannel" || type == typeof(TransportHeaders) ||
                    type.Name == "ActivationListener")
                {
                    continue;
                }
                object obj = null;
                byte[] buffer;
                object deser;
                ConstructorInfo[] constructors = type.GetTypeInfo().GetConstructors();
                ConstructorInfo constructor = constructors.FirstOrDefault(x => x.GetParameters().Length == 0);
                if (constructor != null)
                {
                    bool k = false;
                    try
                    {
                        obj = constructor.Invoke(new object[0]);
                        k = true;
                    }
                    catch
                    {

                    }
                    if (k)
                    {
                        string ff = type.Name;
                        buffer = Serializer.Serialize(obj);
                        deser = Serializer.Deserialize(type, buffer);
                        count++;
                    }
                }
            }
        }
    }
}

