using System;
using System.Reflection;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class EnumRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type?.GetTypeInfo().IsEnum == true)
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
            Type enumtype = Enum.GetUnderlyingType(type);
            bool result = BinarySerializer.TryDeserialize(reader, enumtype, out dynamic _value);
            if (result)
            {
                value = Enum.ToObject(type, _value);
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool TryGetLength(dynamic value, out uint length)
        {
            Type enumtype = Enum.GetUnderlyingType(value.GetType());
            return BinarySerializer.TryGetLength(Convert.ChangeType(value, enumtype), out length);
        }

        public bool TrySerialize(BinaryWriter writer, dynamic value)
        {
            Type enumtype = Enum.GetUnderlyingType(value.GetType());
            return BinarySerializer.TrySerialize(writer, Convert.ChangeType(value, enumtype));
        }
    }
}
