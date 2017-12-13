using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using System.Linq;

namespace EmptyBox.IO.Serializator.Rules
{
    public class NullableRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            //Кажется, можно сделать лучше
            if (type == null || Nullable.GetUnderlyingType(type) != null)
            {
                return SuitabilityDegree.Equal;
            }
            else
            {
                return SuitabilityDegree.NotAssignable;
            }
        }

        public bool TryDeserialize(BinaryReader reader, Type type, out dynamic value)
        {
            bool result = BinarySerializer.Deserialize(reader, out ObjectFlags property);
            if (result)
            {
                switch (property)
                {
                    default:
                    case ObjectFlags.IsNull:
                        value = null;
                        break;
                    case ObjectFlags.None:
                        result &= BinarySerializer.TryDeserialize(reader, type.GetTypeInfo().GenericTypeArguments[0], out dynamic _value);
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

        public bool TryGetLength(dynamic value, out uint length)
        {
            bool result = BinarySerializer.TryGetLength(ObjectFlags.None, out length);
            if (value != null)
            {
                result &= BinarySerializer.TryGetLength(value, out uint _length);
                length += _length;
            }
            return result;
        }

        public bool TrySerialize(BinaryWriter writer, dynamic value)
        {
            if (value != null)
            {
                bool result = BinarySerializer.TrySerialize(writer, ObjectFlags.None);
                result &= BinarySerializer.TrySerialize(writer, value);
                return result;
            }
            else
            {
                return BinarySerializer.TrySerialize(writer, ObjectFlags.IsNull);
            }
        }
    }
}
