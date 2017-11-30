using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using EmptyBox.Extensions;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ObjectRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            return SuitabilityDegree.Assignable;
        }

        public bool Deserialize(BinaryReader reader, Type type, out dynamic value)
        {
            object obj = type.GenerateEmptyObject();
            List<FieldInfo> value_fields = type.GetTypeInfo().DeclaredFields.ToList().FindAll(x => !x.IsStatic && x.IsPublic);
            List<PropertyInfo> value_properties = type.GetTypeInfo().DeclaredProperties.ToList().FindAll(x => x.CanRead && x.CanWrite);
            bool result = BinarySerializer.Deserialize(reader, out uint count);
            foreach (FieldInfo fi in value_fields)
            {
                result &= BinarySerializer.Deserialize(reader, fi.FieldType, out dynamic field);
                fi.SetValue(obj, field);
            }
            foreach (PropertyInfo pi in value_properties)
            {
                result &= BinarySerializer.Deserialize(reader, pi.PropertyType, out dynamic field);
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
            return result;
        }

        public bool GetLength(dynamic value, out int length)
        {
            List<FieldInfo> value_fields;
            List<PropertyInfo> value_properties;
            value_fields = (value as object).GetType().GetTypeInfo().DeclaredFields.ToList().FindAll(x => !x.IsStatic && x.IsPublic);
            value_properties = (value as object).GetType().GetTypeInfo().DeclaredProperties.ToList().FindAll(x => x.CanRead && x.CanWrite);
            bool result = BinarySerializer.GetLength((uint)0, out length);
            foreach (FieldInfo fi in value_fields)
            {
                result &= BinarySerializer.GetLength(fi.GetValue(value), out int _length);
                length += _length;
            }
            foreach (PropertyInfo pi in value_properties)
            {
                result &= BinarySerializer.GetLength(pi.GetValue(value), out int _length);
                length += _length;
            }
            return result;
        }

        public bool Serialize(BinaryWriter writer, dynamic value)
        {
            List<FieldInfo> value_fields;
            List<PropertyInfo> value_properties;
            value_fields = (value as object).GetType().GetTypeInfo().DeclaredFields.ToList().FindAll(x => !x.IsStatic && x.IsPublic);
            value_properties = (value as object).GetType().GetTypeInfo().DeclaredProperties.ToList().FindAll(x => x.CanRead && x.CanWrite);
            uint count = (uint)(value_fields.Count + value_properties.Count);
            bool result = BinarySerializer.Serialize(writer, count);
            foreach (FieldInfo fi in value_fields)
            {
                result &= BinarySerializer.Serialize(writer, fi.GetValue(value));
            }
            foreach (PropertyInfo pi in value_properties)
            {
                result &= BinarySerializer.Serialize(writer, pi.GetValue(value));
            }
            return result;
        }
    }
}
