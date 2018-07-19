using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ULongRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(ulong))
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
            try
            {
                value = reader.ReadUInt64();
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            length = sizeof(ulong);
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            try
            {
                writer.Write((ulong)value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
