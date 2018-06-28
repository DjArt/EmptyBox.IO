using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EmptyBox.IO.Serializator.Rules;

namespace EmptyBox.IO.Serializator
{
    public sealed class BinarySerializer
    {
        private List<IBinarySerializatorRule> Rules;

        public BinarySerializer(Encoding encoding)
        {
            Rules = new List<IBinarySerializatorRule>(128)
            {
                new ArrayRule(),
                new BoolRule(),
                new ByteRule(),
                new CharRule(encoding),
                new DateTimeRule(),
                new DoubleRule(),
                new EnumRule(),
                new FloatRule(),
                new GuidRule(),
                new IDictionaryRule(),
                new IEnumerableRule(),
                new IntRule(),
                new KeyValuePairRule(),
                new LongRule(),
                new NullableRule(),
                new ObjectRule(),
                new SByteRule(),
                new ShortRule(),
                new StringRule(encoding),
                new TimeSpanRule(),
                new UIntRule(),
                new ULongRule(),
                new UShortRule(),
            };
            Rules.ForEach(x => x.BinarySerializer = this);
        }

        public T Deserialize<T>(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(ms);
            bool success = TryDeserialize(reader, out T result);
            if (!success)
            {
                result = default(T);
            }
            reader.Dispose();
            ms.Dispose();
            return result;
        }

        public bool TryDeserialize<T>(BinaryReader reader, out T value)
        {
            bool result = TryDeserialize(reader, typeof(T), out dynamic _value);
            if (result)
            {
                value = (T)_value;
            }
            else
            {
                value = default(T);
            }
            return result;
        }

        public bool TryDeserialize(BinaryReader reader, Type type, out object value)
        {
            IBinarySerializatorRule rule = Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Equal);
            if (rule != null)
            {
                return rule.TryDeserialize(reader, type, out value);
            }
            else
            {
                rule = Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Assignable);
                if (rule != null)
                {
                    return rule.TryDeserialize(reader, type, out value);
                }
                else
                {
                    value = null;
                    return false;
                }
            }
        }

        public bool TryDeserialize<T>(byte[] data, out T value)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(ms);
            bool success = TryDeserialize(reader, out value);
            if (!success)
            {
                value = default(T);
            }
            reader.Dispose();
            ms.Dispose();
            return success;
        }

        public byte[] Serialize<T>(T value)
        {
            TrySerialize(value, out byte[] data);
            return data;
        }

        public bool TrySerialize<T>(T value, out byte[] data)
        {
            return TrySerialize(typeof(T), value, out data);
        }

        public bool TrySerialize(Type type, object value, out byte[] data)
        {
            bool length_success = TryGetLength(type, value, out uint length);
            if (length_success)
            {
                data = new byte[length];
                MemoryStream ms = new MemoryStream(data);
                BinaryWriter writer = new BinaryWriter(ms);
                bool serialize_succes = TrySerialize(writer, type, value);
                if (serialize_succes)
                {
                    writer.Flush();
                    data = ms.ToArray();
                }
                else
                {
                    data = null;
                }
                writer.Dispose();
                ms.Dispose();
                return serialize_succes;
            }
            else
            {
                data = null;
                return false;
            }
        }

        public bool TrySerialize<T>(BinaryWriter writer, T value)
        {
            //Получать тип из value или использовать T?
            return TrySerialize(writer, typeof(T), value);
        }

        public bool TrySerialize(BinaryWriter writer, Type type, object value)
        {
            IBinarySerializatorRule rule = Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Equal);
            if (rule != null)
            {
                return rule.TrySerialize(writer, value);
            }
            else
            {
                rule = Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Assignable);
                if (rule != null)
                {
                    return rule.TrySerialize(writer, value);
                }
                else
                {
                    return false;
                }
            }
        }

        public uint GetLength<T>(T value)
        {
            return GetLength(typeof(T), value);
        }

        public uint GetLength(Type type, object value)
        {
            TryGetLength(type, value, out uint length);
            return length;
        }

        public bool TryGetLength<T>(T value, out uint length)
        {
            return TryGetLength(typeof(T), value, out length);
        }

        public bool TryGetLength(Type type, object value, out uint length)
        {
            IBinarySerializatorRule rule = Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Equal);
            if (rule != null)
            {
                return rule.TryGetLength(value, out length);
            }
            else
            {
                rule = Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Assignable);
                if (rule != null)
                {
                    return rule.TryGetLength(value, out length);
                }
                else
                {
                    length = 0;
                    return false;
                }
            }
        }
    }
}
