using System;
using System.IO;
using System.Reflection;

namespace EmptyBox.IO.Serializator.Rules
{
    public class DelegateRule : ISerializationRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (typeof(Delegate).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
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
            bool result = BinarySerializer.TryDeserialize(reader, out object @null);
            value = null;
            return result;
        }

        public bool TryGetLength(object value, out uint length, string scenario = null, string @case = null)
        {
            return BinarySerializer.TryGetLength<object>(null, out length);
        }

        public bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null)
        {
            return BinarySerializer.TrySerialize<object>(writer, null);
        }
    }
}
