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

        public bool TryDeserialize(BinaryReader reader, Type type, out dynamic value)
        {
            bool result = BinarySerializer.TryDeserialize(reader, out ObjectFlags flag);
            if (result)
            {
                switch (flag)
                {
                    case ObjectFlags.None:
                        Type _type = type.GetElementType();
                        value = null;
                        if (_type == typeof(byte))
                        {
                            try
                            {
                                uint length = reader.ReadUInt32();
                                value = reader.ReadBytes((int)length);
                                result = true;
                            }
                            catch
                            {
                                result = false;
                            }
                        }
                        else if (_type == typeof(char))
                        {
                            result = BinarySerializer.TryDeserialize(reader, out string @string);
                            if (result)
                            {
                                value = @string.ToCharArray();
                            }
                        }
                        else
                        {
                            result = BinarySerializer.TryDeserialize(reader, out int length);
                            if (result)
                            {
                                value = Activator.CreateInstance(_type, new object[] { length });
                                for (int i0 = 0; i0 < length; i0++)
                                {
                                    result &= BinarySerializer.TryDeserialize(reader, _type, out value[i0]);
                                }
                            }
                            if (!result)
                            {
                                value = null;
                            }
                        }
                        break;
                    default:
                    case ObjectFlags.IsNull:
                        value = null;
                        break;
                }
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool TryGetLength(dynamic value, out uint length)
        {
            ObjectFlags flag = value == null ? ObjectFlags.IsNull : ObjectFlags.None;
            bool result = BinarySerializer.TryGetLength(flag, out length);
            switch (flag)
            {
                case ObjectFlags.None:
                    Type type = value?.GetType().GetElementType();
                    if (type == typeof(byte))
                    {
                        result = BinarySerializer.TryGetLength(0, out uint _length);
                        length += (uint)value.Length + _length;
                    }
                    else if (type == typeof(char))
                    {
                        string @string = new string(value);
                        result = BinarySerializer.TryGetLength(@string, out uint _length);
                        length += _length;
                    }
                    else
                    {
                        result = BinarySerializer.TryGetLength(value.Length, out uint _length);
                        length += _length;
                        for (int i0 = 0; i0 < value.Length; i0++)
                        {
                            result &= BinarySerializer.TryGetLength(value[i0], out _length);
                            length += _length;
                        }
                    }
                    break;
                default:
                case ObjectFlags.IsNull:

                    break;
            }
            return result;
        }

        public bool TrySerialize(BinaryWriter writer, dynamic value)
        {
            ObjectFlags flag = value == null ? ObjectFlags.IsNull : ObjectFlags.None;
            bool result = BinarySerializer.TrySerialize(writer, flag);
            switch (flag)
            {
                case ObjectFlags.None:
                    Type type = value.GetType().GetElementType();
                    if (type == typeof(byte))
                    {
                        try
                        {
                            writer.Write((uint)value.Length);
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
                        result = BinarySerializer.TrySerialize(writer, @string);
                    }
                    else
                    {
                        result = BinarySerializer.TrySerialize(writer, value.Length);
                        for (int i0 = 0; i0 < value.Length; i0++)
                        {
                            result &= BinarySerializer.TrySerialize(writer, value[i0]);
                        }
                    }
                    break;
                default:
                case ObjectFlags.IsNull:

                    break;
            }
            return result;
        }
    }
}
