using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ByteRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(byte))
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
                value = reader.ReadByte();
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
            length = sizeof(byte);
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            try
            {
                writer.Write((byte)value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
