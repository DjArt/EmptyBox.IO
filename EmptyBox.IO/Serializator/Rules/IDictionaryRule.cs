using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using EmptyBox.ScriptRuntime.Extensions;
using System.Collections;
using EmptyBox.ScriptRuntime;

namespace EmptyBox.IO.Serializator.Rules
{
    public class IDictionaryRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                return SuitabilityDegree.Equal;
            }
            else if (type.GetTypeInfo().ImplementedInterfaces.Any(x => x.IsConstructedGenericType &&  x.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                return SuitabilityDegree.Assignable;
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
                        Type idicttype;
                        if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                        {
                            idicttype = type;
                        }
                        else
                        {
                            idicttype = type.GetTypeInfo().ImplementedInterfaces.First(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));
                        }
                        Type generictype0 = idicttype.GenericTypeArguments[0];
                        Type generictype1 = idicttype.GenericTypeArguments[1];
                        value = null;
                        result = BinarySerializer.TryDeserialize(reader, out int length);
                        if (result)
                        {
                            //Backported from NET.Standard 1.6+
                            //dynamic tmp = typeof(Dictionary<,>).MakeGenericType(generictype0, generictype1).GetConstructor(new Type[0]).Invoke(new object[0]);
                            dynamic tmp = typeof(Dictionary<,>).MakeGenericType(generictype0, generictype1).GetTypeInfo().DeclaredConstructors.ElementAt(0).Invoke(new object[0]);
                            for (int i0 = 0; i0 < length; i0++)
                            {
                                result &= BinarySerializer.TryDeserialize(reader, generictype0, out dynamic val0);
                                result &= BinarySerializer.TryDeserialize(reader, generictype1, out dynamic val1);
                                tmp.Add(val0, val1);
                            }
                            if (result)
                            {
                                //Backported from NET.Standard 1.6+
                                //ConstructorInfo constructor = type.GetTypeInfo().GetConstructor(new Type[] { typeof(IDictionary<,>).MakeGenericType(generictype0, generictype1) });
                                ConstructorInfo constructor = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault(x => x.GetParameters().Count() == 1 && x.GetParameters()[0].ParameterType == typeof(IDictionary<,>).MakeGenericType(generictype0, generictype1));
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

        public bool TryGetLength(dynamic value, out uint length)
        {
            ObjectFlags flag = value == null ? ObjectFlags.IsNull : ObjectFlags.None;
            bool result = BinarySerializer.TryGetLength(flag, out length);
            switch (flag)
            {
                case ObjectFlags.None:
                    int count = Enumerable.Count(value);
                    result = BinarySerializer.TryGetLength(count, out length);
                    for (int i0 = 0; i0 < count; i0++)
                    {
                        result &= BinarySerializer.TryGetLength(Enumerable.ElementAt(value, i0), out uint _length);
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
            ObjectFlags flag = value == null ? ObjectFlags.IsNull : ObjectFlags.None;
            bool result = BinarySerializer.TrySerialize(writer, flag);
            switch (flag)
            {
                case ObjectFlags.None:
                    Type type = value.GetType().GetElementType();
                    int count = Enumerable.Count(value);
                    result = BinarySerializer.TrySerialize(writer, count);
                    for (int i0 = 0; i0 < count; i0++)
                    {
                        result &= BinarySerializer.TrySerialize(writer, Enumerable.ElementAt(value, i0));
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
