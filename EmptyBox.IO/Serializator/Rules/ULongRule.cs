using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ULongRule : IBinarySerializatorRule
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

        public bool TryDeserialize(BinaryReader reader, Type type, out object value)
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

        public bool TryGetLength(object variable, out uint length)
        {
            length = sizeof(ulong);
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object variable)
        {
            try
            {
                writer.Write((ulong)variable);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
