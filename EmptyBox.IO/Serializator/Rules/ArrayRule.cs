using System;
using System.IO;
using System.Reflection;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ArrayRule : ISerializationRule
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

        public bool TryDeserialize(BinaryReader reader, Type type, out object value, string scenario = null, string @case = null)
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
                            result = BinarySerializer.TryDeserialize(reader, out uint length);
                            if (result)
                            {
                                value = Activator.CreateInstance(_type, new object[] { length });
                                Array array = (Array)value;
                                for (int i0 = 0; i0 < length; i0++)
                                {
                                    result &= BinarySerializer.TryDeserialize(reader, _type, out object _value, scenario, @case);
                                    array.SetValue(_value, i0);
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

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
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
                        length += (uint)((byte[])value).Length + _length;
                    }
                    else if (type == typeof(char))
                    {
                        string @string = new string((char[])value);
                        result = BinarySerializer.TryGetLength(@string, out uint _length);
                        length += _length;
                    }
                    else
                    {
                        Array array = (Array)value;
                        result = BinarySerializer.TryGetLength(array.Length, out uint _length);
                        length += _length;
                        for (int i0 = 0; i0 < array.Length; i0++)
                        {
                            result &= BinarySerializer.TryGetLength(array.GetValue(i0), out _length, scenario, @case);
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

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
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
                            byte[] array = (byte[])value;
                            writer.Write(array.Length);
                            writer.Write(array);
                            result = true;
                        }
                        catch
                        {
                            result = false;
                        }
                    }
                    else if (type == typeof(char))
                    {
                        string @string = new string((char[])value);
                        result = BinarySerializer.TrySerialize(writer, @string);
                    }
                    else
                    {
                        Array array = (Array)value;
                        result = BinarySerializer.TrySerialize(writer, array.Length);
                        for (int i0 = 0; i0 < array.Length; i0++)
                        {
                            result &= BinarySerializer.TrySerialize(writer, array.GetValue(i0), scenario, @case);
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
