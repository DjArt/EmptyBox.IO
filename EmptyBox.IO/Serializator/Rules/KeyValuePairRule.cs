using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator.Rules
{
    public class KeyValuePairRule : IBinarySerializatorRule
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

        public bool Deserialize(BinaryReader reader, Type type, out dynamic value)
        {
            bool result = false;
            Type generictype0 = type.GenericTypeArguments[0];
            Type generictype1 = type.GenericTypeArguments[1];
            value = null;
            try
            {
                result = BinarySerializer.Deserialize(reader, generictype0, out dynamic val0);
                result &= BinarySerializer.Deserialize(reader, generictype1, out dynamic val1);
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

        public bool GetLength(dynamic value, out int length)
        {
            bool result = false;
            length = 0;
            try
            {
                result = BinarySerializer.GetLength(value.Key, out length);
                result &= BinarySerializer.GetLength(value.Value, out int _length);
                length += _length;
            }
            catch
            {

            }
            return result;
        }

        public bool Serialize(BinaryWriter writer, dynamic value)
        {
            bool result = false;
            try
            {
                result = BinarySerializer.Serialize(writer, value.Key);
                result &= BinarySerializer.Serialize(writer, value.Value);
            }
            catch
            {

            }
            return result;
        }
    }
}
