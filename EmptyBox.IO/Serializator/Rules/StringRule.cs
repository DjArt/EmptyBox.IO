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

        public bool TryDeserialize(BinaryReader reader, Type type, out dynamic value)
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

        public bool TryGetLength(dynamic value, out uint length)
        {
            length = Encoding.GetByteCount(value);
            bool result = BinarySerializer.TryGetLength(0, out uint _length);
            length += _length;
            return result;
        }

        public bool TrySerialize(BinaryWriter writer, dynamic value)
        {
            byte[] enc_string = Encoding.GetBytes(value);
            return BinarySerializer.TrySerialize(writer, enc_string);
        }
    }
}
