using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class TimeSpanRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(TimeSpan))
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
            bool result = BinarySerializer.TryDeserialize(reader, out long data);
            if (result)
            {
                value = TimeSpan.FromTicks(data);
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            return BinarySerializer.TryGetLength(TimeSpan.Zero.Ticks, out length);
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            return BinarySerializer.TrySerialize(writer, ((TimeSpan)value).Ticks);
        }
    }
}
