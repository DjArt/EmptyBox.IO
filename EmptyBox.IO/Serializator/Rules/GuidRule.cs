using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class GuidRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(Guid))
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
                byte[] guid = reader.ReadBytes(16);
                value = new Guid(guid);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        public bool TryGetLength(object value, out uint length)
        {
            length = 16;
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object value)
        {
            try
            {
                writer.Write(((Guid)value).ToByteArray());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
