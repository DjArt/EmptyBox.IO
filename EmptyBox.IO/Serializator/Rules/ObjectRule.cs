using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EmptyBox.ScriptRuntime.Extensions;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ObjectRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            return SuitabilityDegree.Assignable;
        }

        public bool TryDeserialize(BinaryReader reader, Type type, out dynamic value)
        {
            bool result = true;
            ObjectFlags flag = ObjectFlags.None;
            if (!type.GetTypeInfo().IsValueType)
            {
                result &= BinarySerializer.TryDeserialize(reader, out flag);
            }
            switch (flag)
            {
                case ObjectFlags.None:
                    object obj = type.GenerateEmptyObject();
                    IEnumerable<FieldInfo> fields = type.GetTypeInfo().DeclaredFields.Where(x => !x.IsStatic && x.IsPublic);
                    IEnumerable<PropertyInfo> properties = type.GetTypeInfo().DeclaredProperties.Where(x => x.CanRead && x.CanWrite);
                    result &= BinarySerializer.TryDeserialize(reader, out uint count);
                    if (count == fields.Count() + properties.Count())
                    {
                        foreach (FieldInfo fi in fields)
                        {
                            result &= BinarySerializer.TryDeserialize(reader, fi.FieldType, out dynamic field);
                            fi.SetValue(obj, field);
                        }
                        foreach (PropertyInfo pi in properties)
                        {
                            result &= BinarySerializer.TryDeserialize(reader, pi.PropertyType, out dynamic field);
                            pi.SetValue(obj, field);
                        }
                        if (result)
                        {
                            value = obj;
                        }
                        else
                        {
                            value = null;
                        }
                    }
                    else
                    {
                        value = null;
                        result = false;
                    }
                    break;
                default:
                case ObjectFlags.IsNull:
                    value = null;
                    result = true;
                    break;
            }
            return result;
        }

        public bool TryGetLength(dynamic value, out uint length)
        {
            bool result = true;
            ObjectFlags flag = ObjectFlags.None;
            TypeInfo typeInfo = value?.GetType();
            length = 0;
            if (typeInfo == null)
            {
                flag = ObjectFlags.IsNull;
                result &= BinarySerializer.TryGetLength(flag, out uint _length);
                length += _length;
            }
            else if (!typeInfo.IsValueType)
            {
                result &= BinarySerializer.TryGetLength(flag, out uint _length);
                length += _length;
            }
            switch (flag)
            {
                case ObjectFlags.None:
                    IEnumerable<FieldInfo> fields = typeInfo.DeclaredFields.Where(x => !x.IsStatic && x.IsPublic);
                    IEnumerable<PropertyInfo> properties = typeInfo.DeclaredProperties.Where(x => x.CanRead && x.CanWrite);
                    uint count = (uint)(fields.Count() + properties.Count());
                    result &= BinarySerializer.TryGetLength(count, out uint _length);
                    length += _length;
                    foreach (FieldInfo fi in fields)
                    {
                        result &= BinarySerializer.TryGetLength(fi.FieldType, fi.GetValue(value), out _length);
                        length += _length;
                    }
                    foreach (PropertyInfo pi in properties)
                    {
                        result &= BinarySerializer.TryGetLength(pi.PropertyType, pi.GetValue(value), out _length);
                        length += _length;
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
            bool result = true;
            ObjectFlags flag = ObjectFlags.None;
            TypeInfo typeInfo = value?.GetType();
            if (typeInfo == null)
            {
                flag = ObjectFlags.IsNull;
                result &= BinarySerializer.TrySerialize(writer, flag);
            }
            else if (!typeInfo.IsValueType)
            {
                result &= BinarySerializer.TrySerialize(writer, flag);
            }
            switch (flag)
            {
                case ObjectFlags.None:
                    IEnumerable<FieldInfo> fields = typeInfo.DeclaredFields.Where(x => !x.IsStatic && x.IsPublic);
                    IEnumerable<PropertyInfo> properties = typeInfo.DeclaredProperties.Where(x => x.CanRead && x.CanWrite);
                    uint count = (uint)(fields.Count() + properties.Count());
                    result &= BinarySerializer.TrySerialize(writer, count);
                    foreach (FieldInfo fi in fields)
                    {
                        result &= BinarySerializer.TrySerialize(writer, fi.FieldType, fi.GetValue(value));
                    }
                    foreach (PropertyInfo pi in properties)
                    {
                        result &= BinarySerializer.TrySerialize(writer, pi.PropertyType, pi.GetValue(value));
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
