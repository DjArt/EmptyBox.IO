using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmptyBox.IO.Serializator
{
    public interface IBinarySerializatorRule
    {
        BinarySerializer BinarySerializer { get; set; }
        SuitabilityDegree CheckSuitability(Type type);
        bool TryDeserialize(BinaryReader reader, Type type, out dynamic value);
        bool TryGetLength(dynamic value, out uint length);
        bool TrySerialize(BinaryWriter writer, dynamic value);
    }
}
