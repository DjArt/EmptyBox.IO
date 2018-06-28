using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ByteRule : IBinarySerializatorRule
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

        public bool TryDeserialize(BinaryReader reader, Type type, out object value)
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

        public bool TryGetLength(object variable, out uint length)
        {
            length = sizeof(byte);
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object variable)
        {
            try
            {
                writer.Write((byte)variable);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
