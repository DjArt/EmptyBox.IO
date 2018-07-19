using System;
using System.IO;

namespace EmptyBox.IO.Serializator
{
    public interface ISerializationRule
    {
        BinarySerializer BinarySerializer { get; set; }
        SuitabilityDegree CheckSuitability(Type type);
        bool TryDeserialize(BinaryReader reader, Type type, out object value, string scenario = null, string @case = null);
        bool TryGetLength(object value, out uint length, string scenario = null, string @case = null);
        bool TrySerialize(BinaryWriter writer, object value, string scenario = null, string @case = null);
    }
}
