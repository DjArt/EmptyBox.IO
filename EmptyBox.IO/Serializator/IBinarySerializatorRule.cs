using System;
using System.IO;

namespace EmptyBox.IO.Serializator
{
    public interface IBinarySerializatorRule
    {
        BinarySerializer BinarySerializer { get; set; }
        SuitabilityDegree CheckSuitability(Type type);
        bool TryDeserialize(BinaryReader reader, Type type, out object value);
        bool TryGetLength(object value, out uint length);
        bool TrySerialize(BinaryWriter writer, object value);
    }
}
