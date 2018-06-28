using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class DateTimeRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(DateTime))
            {
                return SuitabilityDegree.Equal;
            }
            else
            {
                return SuitabilityDegree.NotAssignable;
            }
        }

        public bool TryDeserialize(BinaryReader reader, Type type, out object value)
        {
            bool result = BinarySerializer.TryDeserialize(reader, out long data);
            if (result)
            {
                value = DateTime.FromBinary(data);
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool TryGetLength(object variable, out uint length)
        {
            return BinarySerializer.TryGetLength(DateTime.Now.ToBinary(), out length);
        }

        public bool TrySerialize(BinaryWriter writer, object variable)
        {
            return TrySerialize(writer, ((DateTime)variable).ToBinary());
        }
    }
}
