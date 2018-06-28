using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class UIntRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(uint))
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
                value = reader.ReadUInt32();
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
            length = sizeof(uint);
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object variable)
        {
            try
            {
                writer.Write((uint)variable);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
