﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace EmptyBox.IO.Serializator.Rules
{
    public class IEnumerableRule : ISerializationRule
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

        public bool TryDeserialize(BinaryReader reader, Type type, out object value, string scenario = null, string @case = null)
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
                        value = null;
                        result = BinarySerializer.TryDeserialize(reader, out int length);
                        if (result)
                        {
                            //Backported from NET.Standard 1.6+
                            //dynamic tmp = typeof(List<>).MakeGenericType(generictype).GetConstructor(new Type[0]).Invoke(new object[0]);
                            dynamic tmp = typeof(List<>).MakeGenericType(generictype).GetTypeInfo().DeclaredConstructors.ElementAt(0).Invoke(new object[0]);
                            for (int i0 = 0; result && i0 < length; i0++)
                            {
                                result &= BinarySerializer.TryDeserialize(reader, generictype, out dynamic val0, scenario, @case);
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

        public bool TryGetLength(dynamic value, out uint length, string scenario = null, string @case = null)
        {
            ObjectFlags flag = value == null ? ObjectFlags.IsNull : ObjectFlags.None;
            bool result = BinarySerializer.TryGetLength(flag, out length);
            if (result)
            {
                switch (flag)
                {
                    case ObjectFlags.None:
                        Type type = value.GetType().GenericTypeArguments[0];
                        int count = Enumerable.Count(value);
                        result = BinarySerializer.TryGetLength(count, out uint _length);
                        length += _length;
                        for (int i0 = 0; result && i0 < count; i0++)
                        {
                            result &= BinarySerializer.TryGetLength(type, Enumerable.ElementAt(value, i0), out _length, scenario, @case);
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

        public bool TrySerialize(BinaryWriter writer, dynamic value, string scenario = null, string @case = null)
        {
            ObjectFlags flag = value == null ? ObjectFlags.IsNull : ObjectFlags.None;
            bool result = BinarySerializer.TrySerialize(writer, flag);
            if (result)
            {
                switch (flag)
                {
                    case ObjectFlags.None:
                        Type type = value.GetType().GenericTypeArguments[0];
                        int count = Enumerable.Count(value);
                        result = BinarySerializer.TrySerialize(writer, count);
                        for (int i0 = 0; result && i0 < count; i0++)
                        {
                            result &= BinarySerializer.TrySerialize(writer, type, Enumerable.ElementAt(value, i0), scenario, @case);
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
