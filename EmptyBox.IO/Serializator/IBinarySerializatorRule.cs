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
        bool Deserialize(BinaryReader reader, Type type, out dynamic value);
        bool GetLength(dynamic value, out int length);
        bool Serialize(BinaryWriter writer, dynamic value);
    }
}
