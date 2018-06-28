using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace EmptyBox.IO.Serializator.Rules
{
    public class IEnumerableRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return SuitabilityDegree.Equal;
            }
            else if (type.GetTypeInfo().ImplementedInterfaces.Any(x => x.IsConstructedGenericType &&  x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                return SuitabilityDegree.Assignable;
            }
            else
            {
                return SuitabilityDegree.NotAssignable;
            }
        }

        public bool TryDeserialize(BinaryReader reader, Type type, out object value)
        {
            bool result = BinarySerializer.TryDeserialize(reader, out ObjectFlags flag);
            if (result)
            {
                switch (flag)
                {
                    case ObjectFlags.None:
                        Type ienumtype;
                        if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                        {
                            ienumtype = type;
                        }
                        else
                        {
                            ienumtype = type.GetTypeInfo().ImplementedInterfaces.First(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                        }
                        Type generictype = ienumtype.GenericTypeArguments[0];
                        value = null; result = BinarySerializer.TryDeserialize(reader, out int length);
                        if (result)
                        {
                            //Backported from NET.Standard 1.6+
                            //dynamic tmp = typeof(List<>).MakeGenericType(generictype).GetConstructor(new Type[0]).Invoke(new object[0]);
                            dynamic tmp = typeof(List<>).MakeGenericType(generictype).GetTypeInfo().DeclaredConstructors.ElementAt(0).Invoke(new object[0]);
                            for (int i0 = 0; i0 < length; i0++)
                            {
                                result &= BinarySerializer.TryDeserialize(reader, generictype, out object val0);
                                tmp.Add(val0);
                            }
                            if (result)
                            {
                                //Backported from NET.Standard 1.6+
                                //ConstructorInfo constructor = type.GetTypeInfo().GetConstructor(new Type[] { typeof(IEnumerable<>).MakeGenericType(generictype) });
                                ConstructorInfo constructor = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault(x => x.GetParameters().Count() == 1 && x.GetParameters()[0].ParameterType == typeof(IEnumerable<>).MakeGenericType(generictype));
                                if (constructor != null)
                                {
                                    value = constructor.Invoke(new object[] { tmp });
                                }
                                else
                                {
                                    value = tmp;
                                }
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

        public bool TryGetLength(object value, out uint length)
        {
            ObjectFlags flag = value == null ? ObjectFlags.IsNull : ObjectFlags.None;
            bool result = BinarySerializer.TryGetLength(flag, out length);
            if (result)
            {
                switch (flag)
                {
                    case ObjectFlags.None:
                        IEnumerable<object> _value = (IEnumerable<object>)value;
                        int count = Enumerable.Count(_value);
                        result = BinarySerializer.TryGetLength(count, out length);
                        for (int i0 = 0; i0 < count; i0++)
                        {
                            result &= BinarySerializer.TryGetLength(Enumerable.ElementAt(_value, i0), out uint _length);
                            length += _length;
                        }
                        break;
                    default:
                    case ObjectFlags.IsNull:

                        break;
                }
            }
            return result;
        }

        public bool TrySerialize(BinaryWriter writer, object value)
        {
            ObjectFlags flag = value == null ? ObjectFlags.IsNull : ObjectFlags.None;
            bool result = BinarySerializer.TrySerialize(writer, flag);
            if (result)
            {
                switch (flag)
                {
                    case ObjectFlags.None:
                        IEnumerable<object> _value = (IEnumerable<object>)value;
                        Type type = value.GetType().GetElementType();
                        int count = Enumerable.Count(_value);
                        result = BinarySerializer.TrySerialize(writer, count);
                        for (int i0 = 0; i0 < count; i0++)
                        {
                            result &= BinarySerializer.TrySerialize(writer, Enumerable.ElementAt(_value, i0));
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
    }
}
