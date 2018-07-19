using System;
using System.Collections.Generic;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class KeyValuePairRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
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
            bool result = false;
            Type generictype0 = type.GenericTypeArguments[0];
            Type generictype1 = type.GenericTypeArguments[1];
            value = null;
            try
            {
                result = BinarySerializer.TryDeserialize(reader, generictype0, out object val0, scenario, @case);
                result &= BinarySerializer.TryDeserialize(reader, generictype1, out object val1, scenario, @case);
                if (result)
                {
                    value = Activator.CreateInstance(type, new object[] { val0, val1 });
                }
            }
            catch
            {

            }
            return result;
        }

        public bool TryGetLength(dynamic value, out uint length, string scenario = null, string @case = null)
        {
            bool result = false;
            length = 0;
            try
            {
                result = BinarySerializer.TryGetLength(value.Key, out length, scenario, @case);
                result &= BinarySerializer.TryGetLength(value.Value, out uint _length, scenario, @case);
                length += _length;
            }
            catch
            {

            }
            return result;
        }

        public bool TrySerialize(BinaryWriter writer, dynamic value, string scenario = null, string @case = null)
        {
            bool result = false;
            try
            {
                result = BinarySerializer.TrySerialize(writer, value.Key);
                result &= BinarySerializer.TrySerialize(writer, value.Value);
            }
            catch
            {

            }
            return result;
        }
    }
}
