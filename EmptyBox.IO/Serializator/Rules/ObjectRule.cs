using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EmptyBox.ScriptRuntime.Extensions;

namespace EmptyBox.IO.Serializator.Rules
{
    public class ObjectRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            return SuitabilityDegree.Assignable;
        }

        private bool FieldSearcher(FieldInfo info, string scenario, string @case)
        {
            IEnumerable<Attribute> attributes = info.GetCustomAttributes();
            return !info.IsStatic && info.IsPublic && (!attributes.Any(x => x is SerializationEscapeAttribute) || attributes.Any(x => x is SerializationScenarioAttribute _attr && _attr.Scenario == scenario && _attr.Case == @case));
        }

        private bool PropertySearcher(PropertyInfo info, string scenario = null, string @case = null)
        {
            IEnumerable<Attribute> attributes = info.GetCustomAttributes();
            return info.CanRead && info.CanWrite && (!attributes.Any(x => x is SerializationEscapeAttribute) || attributes.Any(x => x is SerializationScenarioAttribute _attr && _attr.Scenario == scenario && _attr.Case == @case));
        }

        public bool TryDeserialize(BinaryReader reader, Type type, out object value, string scenario = null, string @case = null)
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
                    object obj = type.GetDefault();
                    IEnumerable<FieldInfo> fields = type.GetTypeInfo().DeclaredFields.Where(x => FieldSearcher(x, scenario, @case));
                    IEnumerable<PropertyInfo> properties = type.GetTypeInfo().DeclaredProperties.Where(x => PropertySearcher(x, scenario, @case));
                    result &= BinarySerializer.TryDeserialize(reader, out uint count);
                    if (count == fields.Count() + properties.Count())
                    {
                        foreach (FieldInfo fi in fields)
                        {
                            object field;
                            SerializationScenarioAttribute attr = fi.GetCustomAttributes<SerializationScenarioAttribute>().FirstOrDefault(x => x.Scenario == scenario && x.Case == @case);
                            if (attr != null)
                            {
                                ISerializationScenario wrapper = BinarySerializer.Scenarios.Find(x => x.Name == scenario);
                                MethodInfo method = wrapper.GetType().GetTypeInfo().GetDeclaredMethod("Unwrap").MakeGenericMethod(attr.WrappedType, fi.FieldType);
                                result &= BinarySerializer.TryDeserialize(reader, attr.WrappedType, out field);
                                field = method.Invoke(wrapper, new object[] { field, @case });
                            }
                            else
                            {
                                result &= BinarySerializer.TryDeserialize(reader, fi.FieldType, out field);
                            }
                            fi.SetValue(obj, field);
                        }
                        foreach (PropertyInfo pi in properties)
                        {
                            object field;
                            SerializationScenarioAttribute attr = pi.GetCustomAttributes<SerializationScenarioAttribute>().FirstOrDefault(x => x.Scenario == scenario && x.Case == @case);
                            if (attr != null)
                            {
                                ISerializationScenario wrapper = BinarySerializer.Scenarios.Find(x => x.Name == scenario);
                                MethodInfo method = wrapper.GetType().GetTypeInfo().GetDeclaredMethod("Unwrap").MakeGenericMethod(attr.WrappedType, pi.PropertyType);
                                result &= BinarySerializer.TryDeserialize(reader, attr.WrappedType, out field);
                                field = method.Invoke(wrapper, new object[] { field, @case });
                            }
                            else
                            {
                                result &= BinarySerializer.TryDeserialize(reader, pi.PropertyType, out field);
                            }
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

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            bool result = true;
            ObjectFlags flag = ObjectFlags.None;
            TypeInfo typeInfo = value?.GetType().GetTypeInfo();
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
                    IEnumerable<FieldInfo> fields = typeInfo.DeclaredFields.Where(x => FieldSearcher(x, scenario, @case));
                    IEnumerable<PropertyInfo> properties = typeInfo.DeclaredProperties.Where(x => PropertySearcher(x, scenario, @case));
                    uint count = (uint)(fields.Count() + properties.Count());
                    result &= BinarySerializer.TryGetLength(count, out uint _length);
                    length += _length;
                    foreach (FieldInfo fi in fields)
                    {
                        SerializationScenarioAttribute attr = fi.GetCustomAttributes<SerializationScenarioAttribute>().FirstOrDefault(x => x.Scenario == scenario && x.Case == @case);
                        if (attr != null)
                        {
                            ISerializationScenario wrapper = BinarySerializer.Scenarios.Find(x => x.Name == scenario);
                            MethodInfo method = wrapper.GetType().GetTypeInfo().GetDeclaredMethod("Wrap").MakeGenericMethod(fi.FieldType, attr.WrappedType);
                            object field = method.Invoke(wrapper, new object[] { fi.GetValue(value), @case });
                            result &= BinarySerializer.TryGetLength(attr.WrappedType, field, out _length);
                        }
                        else
                        {
                            result &= BinarySerializer.TryGetLength(fi.FieldType, fi.GetValue(value), out _length);
                        }
                        length += _length;
                    }
                    foreach (PropertyInfo pi in properties)
                    {
                        SerializationScenarioAttribute attr = pi.GetCustomAttributes<SerializationScenarioAttribute>().FirstOrDefault(x => x.Scenario == scenario && x.Case == @case);
                        if (attr != null)
                        {
                            ISerializationScenario wrapper = BinarySerializer.Scenarios.Find(x => x.Name == scenario);
                            MethodInfo method = wrapper.GetType().GetTypeInfo().GetDeclaredMethod("Wrap").MakeGenericMethod(pi.PropertyType, attr.WrappedType);
                            object field = method.Invoke(wrapper, new object[] { pi.GetValue(value), @case });
                            result &= BinarySerializer.TryGetLength(attr.WrappedType, field, out _length);
                        }
                        else
                        {
                            result &= BinarySerializer.TryGetLength(pi.PropertyType, pi.GetValue(value), out _length);
                        }
                        length += _length;
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
            bool result = true;
            ObjectFlags flag = ObjectFlags.None;
            TypeInfo typeInfo = value?.GetType().GetTypeInfo();
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
                    IEnumerable<FieldInfo> fields = typeInfo.DeclaredFields.Where(x => FieldSearcher(x, scenario, @case));
                    IEnumerable<PropertyInfo> properties = typeInfo.DeclaredProperties.Where(x => PropertySearcher(x, scenario, @case));
                    uint count = (uint)(fields.Count() + properties.Count());
                    result &= BinarySerializer.TrySerialize(writer, count);
                    foreach (FieldInfo fi in fields)
                    {
                        SerializationScenarioAttribute attr = fi.GetCustomAttributes<SerializationScenarioAttribute>().FirstOrDefault(x => x.Scenario == scenario && x.Case == @case);
                        if (attr != null)
                        {
                            ISerializationScenario wrapper = BinarySerializer.Scenarios.Find(x => x.Name == scenario);
                            MethodInfo method = wrapper.GetType().GetTypeInfo().GetDeclaredMethod("Wrap").MakeGenericMethod(fi.FieldType, attr.WrappedType);
                            object field = method.Invoke(wrapper, new object[] { fi.GetValue(value), @case });
                            result &= BinarySerializer.TrySerialize(writer, attr.WrappedType, field);
                        }
                        else
                        {
                            result &= BinarySerializer.TrySerialize(writer, fi.FieldType, fi.GetValue(value));
                        }
                    }
                    foreach (PropertyInfo pi in properties)
                    {
                        SerializationScenarioAttribute attr = pi.GetCustomAttributes<SerializationScenarioAttribute>().FirstOrDefault(x => x.Scenario == scenario && x.Case == @case);
                        if (attr != null)
                        {
                            ISerializationScenario wrapper = BinarySerializer.Scenarios.Find(x => x.Name == scenario);
                            MethodInfo method = wrapper.GetType().GetTypeInfo().GetDeclaredMethod("Wrap").MakeGenericMethod(pi.PropertyType, attr.WrappedType);
                            object field = method.Invoke(wrapper, new object[] { pi.GetValue(value), @case });
                            result &= BinarySerializer.TrySerialize(writer, attr.WrappedType, field);
                        }
                        else
                        {
                            result &= BinarySerializer.TrySerialize(writer, pi.PropertyType, pi.GetValue(value));
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
