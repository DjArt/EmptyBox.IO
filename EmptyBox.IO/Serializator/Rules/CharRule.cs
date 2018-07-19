﻿using System;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class CharRule : ISerializationRule
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

        public bool TryDeserialize(BinaryReader reader, Type type, out object value, string scenario = null, string @case = null)
        {
            bool result = BinarySerializer.TryDeserialize(reader, out byte[] enc_char);
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

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            length = (uint)Encoding.GetByteCount(new char[] { (char)value });
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            byte[] enc_char = Encoding.GetBytes(new char[] { (char)value });
            return BinarySerializer.TrySerialize(writer, enc_char);
        }
    }
}
