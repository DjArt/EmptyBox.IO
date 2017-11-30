using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ArrayRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type?.GetTypeInfo().IsArray == true)
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
            bool result = false;
            Type _type = type.GetElementType();
            value = null;
            if (_type == typeof(byte))
            {
                try
                {
                    int length = reader.ReadInt32();
                    value = reader.ReadBytes(length);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            else if (_type == typeof(char))
            {
                result = BinarySerializer.Deserialize(reader, out string @string);
                if (result)
                {
                    value = @string.ToCharArray();
                }
            }
            else
            {
                result = BinarySerializer.Deserialize(reader, out int length);
                if (result)
                {
                    value = Activator.CreateInstance(_type, new object[] { length });
                    for (int i0 = 0; i0 < length; i0++)
                    {
                        result &= BinarySerializer.Deserialize(reader, _type, out value[i0]);
                    }
                }
                if (!result)
                {
                    value = null;
                }
            }
            return result;
        }

        public bool GetLength(dynamic value, out int length)
        {
            Type type = value.GetType().GetElementType();
            bool result = false;
            if (type == typeof(byte))
            {
                result = BinarySerializer.GetLength(0, out int _length);
                length = value.Length + _length;
            }
            else if (type == typeof(char))
            {
                string @string = new string(value);
                result = BinarySerializer.GetLength(@string, out length);
            }
            else
            {
                result = BinarySerializer.GetLength(value.Length, out length);
                for (int i0 = 0; i0 < value.Length; i0++)
                {
                    result &= BinarySerializer.GetLength(value[i0], out int _length);
                    length += _length;
                }
            }
            return result;
        }

        public bool Serialize(BinaryWriter writer, dynamic value)
        {
            Type type = value.GetType().GetElementType();
            bool result = false;
            if (type == typeof(byte))
            {
                try
                {
                    writer.Write(value.Length);
                    writer.Write(value);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            else if (type == typeof(char))
            {
                string @string = new string(value);
                result = BinarySerializer.Serialize(writer, @string);
            }
            else
            {
                result = BinarySerializer.Serialize(writer, value.Length);
                for (int i0 = 0; i0 < value.Length; i0++)
                {
                    result &= BinarySerializer.Serialize(writer, value[i0]);
                }
            }
            return result;
        }
    }
}
