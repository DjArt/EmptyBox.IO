using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using EmptyBox.IO.Serializator.Rules;

namespace EmptyBox.IO.Serializator
{
    public sealed class BinarySerializer
    {
        private List<ISerializationRule> _Rules { get; set; }

        public ReadOnlyCollection<ISerializationRule> Rules => _Rules.AsReadOnly();
        public List<ISerializationScenario> Scenarios { get; private set; }

        public BinarySerializer(Encoding encoding)
        {
            _Rules = new List<ISerializationRule>(128)
            {
                new ArrayRule(),
                new BooleanRule(),
                new ByteRule(),
                new CharRule(encoding),
                new DateTimeRule(),
                new DelegateRule(),
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
            _Rules.ForEach(x => x.BinarySerializer = this);
            Scenarios = new List<ISerializationScenario>();
        }

        public void AddRule(ISerializationRule rule)
        {
            _Rules.Add(rule);
            rule.BinarySerializer = this;
        }

        public void RemoveRule(ISerializationRule rule)
        {
            _Rules.Remove(rule);
            rule.BinarySerializer = null;
        }

        public T Deserialize<T>(byte[] data, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                MemoryStream ms = new MemoryStream(data);
                BinaryReader reader = new BinaryReader(ms);
                bool success = TryDeserialize(reader, out T result, scenario, @case);
                if (!success)
                {
                    result = default(T);
                }
                reader.Dispose();
                ms.Dispose();
                return result;
            }
        }

        public object Deserialize(Type type, byte[] data, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                MemoryStream ms = new MemoryStream(data);
                BinaryReader reader = new BinaryReader(ms);
                bool success = TryDeserialize(reader, type, out object result, scenario, @case);
                if (!success)
                {
                    result = null;
                }
                reader.Dispose();
                ms.Dispose();
                return result;
            }
        }

        public bool TryDeserialize<T>(BinaryReader reader, out T value, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                bool result = TryDeserialize(reader, typeof(T), out dynamic _value, scenario, @case);
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
        }

        public bool TryDeserialize(BinaryReader reader, Type type, out object value, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                ISerializationRule rule = _Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Equal);
                if (rule != null)
                {
                    return rule.TryDeserialize(reader, type, out value, scenario, @case);
                }
                else
                {
                    rule = _Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Assignable);
                    if (rule != null)
                    {
                        return rule.TryDeserialize(reader, type, out value, scenario, @case);
                    }
                    else
                    {
                        value = null;
                        return false;
                    }
                }
            }
        }

        public bool TryDeserialize<T>(byte[] data, out T value, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                MemoryStream ms = new MemoryStream(data);
                BinaryReader reader = new BinaryReader(ms);
                bool success = TryDeserialize(reader, out value, scenario, @case);
                if (!success)
                {
                    value = default(T);
                }
                reader.Dispose();
                ms.Dispose();
                return success;
            }
        }

        public byte[] Serialize<T>(T value, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                TrySerialize(value, out byte[] data, scenario, @case);
                return data;
            }
        }

        public bool TrySerialize<T>(T value, out byte[] data, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                return TrySerialize(value.GetType(), value, out data, scenario, @case);
            }
        }

        public bool TrySerialize(Type type, object value, out byte[] data, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                bool length_success = TryGetLength(type, value, out uint length, scenario, @case);
                if (length_success)
                {
                    data = new byte[length];
                    MemoryStream ms = new MemoryStream(data);
                    BinaryWriter writer = new BinaryWriter(ms);
                    bool serialize_succes = TrySerialize(writer, type, value, scenario, @case);
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
        }

        public bool TrySerialize<T>(BinaryWriter writer, T value, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                return TrySerialize(writer, typeof(T), value, scenario, @case);
            }
        }

        public bool TrySerialize(BinaryWriter writer, Type type, object value, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                ISerializationRule rule = _Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Equal);
                if (rule != null)
                {
                    return rule.TrySerialize(writer, value, scenario, @case);
                }
                else
                {
                    rule = _Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Assignable);
                    if (rule != null)
                    {
                        return rule.TrySerialize(writer, value, scenario, @case);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public uint GetLength<T>(T value, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                return GetLength(typeof(T), value, scenario, @case);
            }
        }

        public uint GetLength(Type type, object value, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                TryGetLength(type, value, out uint length, scenario, @case);
                return length;
            }
        }

        public bool TryGetLength<T>(T value, out uint length, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                return TryGetLength(typeof(T), value, out length, scenario, @case);
            }
        }

        public bool TryGetLength(Type type, object value, out uint length, string scenario = null, string @case = null)
        {
            if (scenario != null && !Scenarios.Exists(x => x.Name == scenario))
            {
                throw new MissingMethodException($"Scenario {scenario} is missing!");
            }
            else
            {
                ISerializationRule rule = _Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Equal);
                if (rule != null)
                {
                    return rule.TryGetLength(value, out length, scenario, @case);
                }
                else
                {
                    rule = _Rules.Find(x => x.CheckSuitability(type) == SuitabilityDegree.Assignable);
                    if (rule != null)
                    {
                        return rule.TryGetLength(value, out length, scenario, @case);
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
}
