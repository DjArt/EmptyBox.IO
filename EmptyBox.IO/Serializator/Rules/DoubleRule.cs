using System;
using System.IO;

namespace EmptyBox.IO.Serializator.Rules
{
    public class DoubleRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type == typeof(double))
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
            try
            {
                value = reader.ReadDouble();
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            length = sizeof(double);
            return true;
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            try
            {
                writer.Write((double)value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
