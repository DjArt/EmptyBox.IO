using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public bool Deserialize(BinaryReader reader, Type type, out dynamic value)
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

        public bool GetLength(dynamic value, out int length)
        {
            length = 16;
            return true;
        }

        public bool Serialize(BinaryWriter writer, dynamic value)
        {
            try
            {
                writer.Write(value.ToByteArray());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
