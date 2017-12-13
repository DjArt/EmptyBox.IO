using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using EmptyBox.IO.Serializator.Rules;

namespace EmptyBox.IO.Serializator
{
    public class BinarySerializer
    {
        protected List<IBinarySerializatorRule> Rules;

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
            bool success = Deserialize(reader, out T result);
            if (!success)
            {
                result = default(T);
            }
            reader.Dispose();
            ms.Dispose();
            return result;
        }

        public bool Deserialize<T>(BinaryReader reader, out T value)
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

        public bool TryDeserialize(BinaryReader reader, Type type, out dynamic value)
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
            bool success = Deserialize(reader, out value);
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
            bool length_success = TryGetLength(value, out uint length);
            if (length_success)
            {
                byte[] data = new byte[length];
                MemoryStream ms = new MemoryStream(data);
                BinaryWriter writer = new BinaryWriter(ms);
                bool serialize_succes = TrySerialize(writer, value);
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
                return data;
            }
            else
            {
                return null;
            }
        }

        public bool TrySerialize<T>(T value, out byte[] data)
        {
            bool length_success = TryGetLength(value, out uint length);
            if (length_success)
            {
                data = new byte[length];
                MemoryStream ms = new MemoryStream(data);
                BinaryWriter writer = new BinaryWriter(ms);
                bool serialize_succes = TrySerialize(writer, value);
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
            IBinarySerializatorRule rule = Rules.Find(x => x.CheckSuitability(value?.GetType()) == SuitabilityDegree.Equal);
            if (rule != null)
            {
                return rule.TrySerialize(writer, value);
            }
            else
            {
                rule = Rules.Find(x => x.CheckSuitability(value?.GetType()) == SuitabilityDegree.Assignable);
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
            IBinarySerializatorRule rule = Rules.Find(x => x.CheckSuitability(value?.GetType()) == SuitabilityDegree.Equal);
            if (rule != null)
            {
                if (rule.TryGetLength(value, out uint length))
                {
                    return length;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                rule = Rules.Find(x => x.CheckSuitability(value?.GetType()) == SuitabilityDegree.Assignable);
                if (rule != null)
                {
                    if (rule.TryGetLength(value, out uint length))
                    {
                        return length;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool TryGetLength<T>(T value, out uint length)
        {
            IBinarySerializatorRule rule = Rules.Find(x => x.CheckSuitability(value?.GetType()) == SuitabilityDegree.Equal);
            if (rule != null)
            {
                return rule.TryGetLength(value, out length);
            }
            else
            {
                rule = Rules.Find(x => x.CheckSuitability(value?.GetType()) == SuitabilityDegree.Assignable);
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
