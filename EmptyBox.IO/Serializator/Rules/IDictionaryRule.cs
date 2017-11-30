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
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                return SuitabilityDegree.Equal;
            }
            else if (type.GetTypeInfo().ImplementedInterfaces.Any(x => x.IsGenericType &&  x.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                return SuitabilityDegree.Assignable;
            }
            else
            {
                return SuitabilityDegree.NotAssignable;
            }
        }

        public bool Deserialize(BinaryReader reader, Type type, out dynamic value)
        {
            bool result = false;
            Type idicttype;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                idicttype = type;
            }
            else
            {
                idicttype = type.GetTypeInfo().ImplementedInterfaces.First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            }
            Type generictype0 = idicttype.GenericTypeArguments[0];
            Type generictype1 = idicttype.GenericTypeArguments[1];
            value = null;
            try
            {
                result = BinarySerializer.Deserialize(reader, out int length);
                if (result)
                {
                    dynamic tmp = typeof(Dictionary<,>).MakeGenericType(generictype0, generictype1).GetConstructor(new Type[0]).Invoke(new object[0]);
                    for (int i0 = 0; i0 < length; i0++)
                    {
                        result &= BinarySerializer.Deserialize(reader, generictype0, out dynamic val0);
                        result &= BinarySerializer.Deserialize(reader, generictype1, out dynamic val1);
                        tmp.Add(val0, val1);
                    }
                    if (result)
                    {
                        ConstructorInfo constructor = type.GetTypeInfo().GetConstructor(new Type[] { typeof(IDictionary<,>).MakeGenericType(generictype0, generictype1) });
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
            }
            catch
            {

            }
            return result;
        }

        public bool GetLength(dynamic value, out int length)
        {
            bool result = false;
            try
            {
                int count = Enumerable.Count(value);
                result = BinarySerializer.GetLength(count, out length);
                for (int i0 = 0; i0 < count; i0++)
                {
                    result &= BinarySerializer.GetLength(Enumerable.ElementAt(value, i0), out int _length);
                    length += _length;
                }
            }
            catch
            {
                length = 0;
            }
            return result;
        }

        public bool Serialize(BinaryWriter writer, dynamic value)
        {
            Type type = value.GetType().GetElementType();
            bool result = false;
            try
            {
                int count = Enumerable.Count(value);
                result = BinarySerializer.Serialize(writer, count);
                for (int i0 = 0; i0 < count; i0++)
                {
                    result &= BinarySerializer.Serialize(writer, Enumerable.ElementAt(value, i0));
                }
            }
            catch
            {

            }
            return result;
        }
    }
}
