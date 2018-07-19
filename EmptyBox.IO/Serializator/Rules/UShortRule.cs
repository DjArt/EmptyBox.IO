using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class UShortRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(ushort))
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
                value = reader.ReadUInt16();
                return true;
            }
            catch
            {
                value = 0;
                return false;
            }
        }

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            length = sizeof(ushort);
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            try
            {
                writer.Write((ushort)value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
