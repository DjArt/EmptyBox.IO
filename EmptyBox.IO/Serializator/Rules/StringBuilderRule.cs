using System;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class StringBuilderRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(StringBuilder))
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
            bool result = BinarySerializer.TryDeserialize(reader, out string data);
            if (result)
            {
                value = new StringBuilder(data);
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            return BinarySerializer.TryGetLength(value == null ? null : ((StringBuilder)value).ToString(), out length);
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            return BinarySerializer.TrySerialize(writer, value == null ? null : ((StringBuilder)value).ToString());
        }
    }
}
