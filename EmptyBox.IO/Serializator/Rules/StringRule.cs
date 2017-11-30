using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class StringRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }
        public Encoding Encoding { get; }
        
        public StringRule(Encoding encoding)
        {
            Encoding = encoding;
        }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(string))
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
            bool result = BinarySerializer.Deserialize(reader, out byte[] enc_string);
            if (result)
            {
                value = Encoding.GetString(enc_string);
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool GetLength(dynamic value, out int length)
        {
            length = Encoding.GetByteCount(value);
            bool result = BinarySerializer.GetLength(0, out int _length);
            length += _length;
            return result;
        }

        public bool Serialize(BinaryWriter writer, dynamic value)
        {
            byte[] enc_string = Encoding.GetBytes(value);
            return BinarySerializer.Serialize(writer, enc_string);
        }
    }
}
