using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public bool Deserialize(BinaryReader reader, Type type, out dynamic value)
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

        public bool GetLength(dynamic variable, out int length)
        {
            length = sizeof(uint);
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
