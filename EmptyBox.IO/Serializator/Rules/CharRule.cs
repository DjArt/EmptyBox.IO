using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class CharRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }
        public Encoding Encoding { get; }
        
        public CharRule(Encoding encoding)
        {
            Encoding = encoding;
        }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(char))
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
            bool result = BinarySerializer.Deserialize(reader, out byte[] enc_char);
            if (result)
            {
                value = Encoding.GetChars(enc_char)[0];
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool TryGetLength(dynamic value, out uint length)
        {
            length = (uint)Encoding.GetByteCount(new char[] { value });
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, dynamic value)
        {
            byte[] enc_char = Encoding.GetBytes(new char[] { value });
            return BinarySerializer.TrySerialize(writer, enc_char);
        }
    }
}
