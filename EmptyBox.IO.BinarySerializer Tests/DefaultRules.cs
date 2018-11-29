using System;
using System.Text;
using EmptyBox.IO.Serializator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmptyBox.ScriptRuntime.Extensions;
using System.Collections.Generic;

namespace EmptyBox.IO.BinarySerializer_Tests
{
    [TestClass]
    public class DefaultRules
    {
        BinarySerializer Serializer = new BinarySerializer(Encoding.UTF32);
        Random rand = new Random();

        [TestMethod]
        public void ArrayRule_Byte()
        {
            byte[] buffer;

            byte[] data0 = null;
            byte[] data1 = new byte[0];
            byte[] data2 = new byte[rand.Next(1, 1024)]; rand.NextBytes(data2);

            buffer = Serializer.Serialize(data0);
            Assert.IsTrue(Serializer.Deserialize<byte[]>(buffer) == null);

            buffer = Serializer.Serialize(data1);
            Assert.IsTrue(data1.SetEqual(Serializer.Deserialize<byte[]>(buffer)));

            buffer = Serializer.Serialize(data2);
            Assert.IsTrue(data2.SetEqual(Serializer.Deserialize<byte[]>(buffer)));
        }

        [TestMethod]
        public void ArrayRule_Char()
        {
            byte[] buffer;

            char[] data0 = null;
            char[] data1 = new char[0];
            buffer = new byte[rand.Next(1, 1024) * 2]; rand.NextBytes(buffer);
            char[] data2 = Encoding.Unicode.GetChars(buffer);
            buffer = null;

            buffer = Serializer.Serialize(data0);
            Assert.IsTrue(Serializer.Deserialize<byte[]>(buffer) == null);

            buffer = Serializer.Serialize(data1);
            Assert.IsTrue(data1.SetEqual(Serializer.Deserialize<char[]>(buffer)));

            buffer = Serializer.Serialize(data2);
            Assert.IsTrue(data2.SetEqual(Serializer.Deserialize<char[]>(buffer)));
        }

        [TestMethod]
        public void ArrayRule_DateTime()
        {
            byte[] buffer;

            DateTime[] data0 = null;
            DateTime[] data1 = new DateTime[0];
            DateTime[] data2 = new DateTime[rand.Next(1, 1024)];
            for (int i0 = 0; i0 < data2.Length; i0++) data2[i0] = new DateTime((long)(rand.NextDouble() * DateTime.MaxValue.Ticks));

            buffer = Serializer.Serialize(data0);
            Assert.IsTrue(Serializer.Deserialize<byte[]>(buffer) == null);

            buffer = Serializer.Serialize(data1);
            Assert.IsTrue(data1.SetEqual(Serializer.Deserialize<DateTime[]>(buffer)));

            buffer = Serializer.Serialize(data2);
            Assert.IsTrue(data2.SetEqual(Serializer.Deserialize<DateTime[]>(buffer)));
        }

        [TestMethod]
        public void ArrayRule_Nullable_DateTime()
        {
            byte[] buffer;

            DateTime?[] data0 = null;
            DateTime?[] data1 = new DateTime?[0];
            DateTime?[] data2 = new DateTime?[rand.Next(1, 1024)];
            for (int i0 = 0; i0 < data2.Length; i0++) data2[i0] = new DateTime((long)(rand.NextDouble() * DateTime.MaxValue.Ticks));

            buffer = Serializer.Serialize(data0);
            Assert.IsTrue(Serializer.Deserialize<DateTime?[]>(buffer) == null);

            buffer = Serializer.Serialize(data1);
            Assert.IsTrue(data1.SetEqual(Serializer.Deserialize<DateTime?[]>(buffer)));

            buffer = Serializer.Serialize(data2);
            Assert.IsTrue(data2.SetEqual(Serializer.Deserialize<DateTime?[]>(buffer)));
        }

        [TestMethod]
        public void IEnumerableRule_Nullable_DateTime()
        {
            byte[] buffer;

            List<DateTime?> data0 = null;
            List<DateTime?> data1 = new List<DateTime?>(0);
            List<DateTime?> data2 = new List<DateTime?>(rand.Next(1, 1024));
            for (int i0 = 0; i0 < data2.Capacity; i0++) data2.Add(new DateTime((long)(rand.NextDouble() * DateTime.MaxValue.Ticks)));

            buffer = Serializer.Serialize(data0);
            Assert.IsTrue(Serializer.Deserialize<List<DateTime?>>(buffer) == null);

            buffer = Serializer.Serialize(data1);
            Assert.IsTrue(data1.SetEqual(Serializer.Deserialize<List<DateTime?>>(buffer)));

            buffer = Serializer.Serialize(data2);
            Assert.IsTrue(data2.SetEqual(Serializer.Deserialize<List<DateTime?>>(buffer)));
        }
    }
}
