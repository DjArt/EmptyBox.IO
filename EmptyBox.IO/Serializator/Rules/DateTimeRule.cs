using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class DateTimeRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(DateTime))
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
            bool result = BinarySerializer.Deserialize(reader, out long data);
            if (result)
            {
                value = DateTime.FromBinary(data);
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool GetLength(dynamic variable, out int length)
        {
            return BinarySerializer.GetLength(DateTime.Now.ToBinary(), out length);
        }

        public bool Serialize(BinaryWriter writer, dynamic variable)
        {
            return BinarySerializer.Serialize(writer, variable.ToBinary());
        }
    }
}
