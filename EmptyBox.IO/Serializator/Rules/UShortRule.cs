using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class UShortRule : IBinarySerializatorRule
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

        public bool TryDeserialize(BinaryReader reader, Type type, out dynamic value)
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

        public bool TryGetLength(dynamic variable, out uint length)
        {
            length = sizeof(ushort);
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, dynamic variable)
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
