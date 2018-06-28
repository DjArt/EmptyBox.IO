using System;
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

        public bool TryDeserialize(BinaryReader reader, Type type, out object value)
        {
            bool result = BinarySerializer.TryDeserialize(reader, out byte[] enc_string);
            if (result)
            {
                if (enc_string != null)
                {
                    value = Encoding.GetString(enc_string);
                }
                else
                {
                    value = string.Empty;
                }
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool TryGetLength(object value, out uint length)
        {
            //Get length of byte array
            bool result = BinarySerializer.TryGetLength(new byte[0], out length);
            if (value != null && (string)value != string.Empty)
            {
                //Get length of string
                length += (uint)Encoding.GetByteCount((string)value);
            }
            return result;
        }

        public bool TrySerialize(BinaryWriter writer, object value)
        {
            byte[] enc_string = null;
            if (value != null && (string)value != string.Empty)
            {
                enc_string = Encoding.GetBytes((string)value);
            }
            return BinarySerializer.TrySerialize(writer, enc_string);
        }
    }
}
