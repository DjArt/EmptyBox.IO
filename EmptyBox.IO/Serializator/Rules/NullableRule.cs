using System;
using System.IO;
using System.Reflection;

namespace EmptyBox.IO.Serializator.Rules
{
    public class NullableRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == null || Nullable.GetUnderlyingType(type) != null)
            {
                return SuitabilityDegree.Equal;
            }
            else
            {
                return SuitabilityDegree.NotAssignable;
            }
        }

        public bool TryDeserialize(BinaryReader reader, Type type, out object value, string scenario = null, string @case = null)
        {
            bool result = BinarySerializer.TryDeserialize(reader, out ObjectFlags property);
            if (result)
            {
                switch (property)
                {
                    default:
                    case ObjectFlags.IsNull:
                        value = null;
                        break;
                    case ObjectFlags.None:
                        result &= BinarySerializer.TryDeserialize(reader, type.GetTypeInfo().GenericTypeArguments[0], out object _value, scenario, @case);
                        if (result)
                        {
                            value = _value;
                        }
                        else
                        {
                            value = null;
                        }
                        break;
                }
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            bool result = BinarySerializer.TryGetLength(ObjectFlags.None, out length);
            if (value != null)
            {
                Type type = value.GetType();
                result &= BinarySerializer.TryGetLength(type, value, out uint _length, scenario, @case);
                length += _length;
            }
            return result;
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            if (value != null)
            {
                bool result = BinarySerializer.TrySerialize(writer, ObjectFlags.None);
                Type type = value.GetType();
                result &= BinarySerializer.TrySerialize(writer, type, value, scenario, @case);
                return result;
            }
            else
            {
                return BinarySerializer.TrySerialize(writer, ObjectFlags.IsNull);
            }
        }
    }
}
