using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class SByteRule : IBinarySerializatorRule
    {
        public  BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(sbyte))
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
                value = reader.ReadSByte();
                return true;
            }
            catch
            {
                value = 0;
                return false;
            }
        }

        public bool GetLength(dynamic variable, out int length)
        {
            length = sizeof(sbyte);
            return true;
        }

        public bool Serialize(BinaryWriter writer, dynamic variable)
        {
            try
            {
                writer.Write(variable);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
