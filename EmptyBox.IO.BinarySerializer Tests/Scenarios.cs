using EmptyBox.IO.Serializator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyBox.ScriptRuntime.Extensions;

namespace EmptyBox.IO.BinarySerializer_Tests
{
    [TestClass]
    public class Scenarios
    {
        public struct Value
        {
            [SerializationScenario("Test0", typeof(int))]
            public string Value0 { get; set; }

            public override string ToString()
            {
                return Value0;
            }
        }

        public class Test0 : ISerializationScenario
        {
            public string Name => "Test0";

            public TOut Unwrap<TIn, TOut>(TIn x, string @case)
            {
                if (typeof(TIn) == typeof(int) && typeof(TOut) == typeof(string))
                {
                    return (TOut)(object)x.ToString();
                }
                else
                {
                    return default;
                }
            }

            public TOut Wrap<TIn, TOut>(TIn x, string @case)
            {
                if (typeof(TIn) == typeof(string) && typeof(TOut) == typeof(int))
                {
                    return (TOut)(object)int.Parse(x.ToString());
                }
                else
                {
                    return default;
                }
            }
        }

        BinarySerializer Serializer = new BinarySerializer(Encoding.UTF32);
        Random rand = new Random();

        public Scenarios()
        {
            Serializer.Scenarios.Add(new Test0());
        }

        [TestMethod]
        public void StructTest()
        {
            byte[] buffer;

            Value data0 = new Value() { Value0 = "123456" };
            buffer = Serializer.Serialize(data0);
            Assert.AreEqual(data0, Serializer.Deserialize<Value>(buffer));

            buffer = Serializer.Serialize(data0, "Test0");
            Assert.AreEqual(data0, Serializer.Deserialize<Value>(buffer, "Test0"));

        }
    }
}
