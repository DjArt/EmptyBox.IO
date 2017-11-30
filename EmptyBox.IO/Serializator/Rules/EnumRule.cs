using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Runtime.CompilerServices;
using EmptyBox.Utils;

namespace EmptyBox.IO.Serializator.Rules
{
    public class EnumRule : IBinarySerializatorRule
    {
        public BinarySerializer BinarySerializer { get; set; }

        public SuitabilityDegree CheckSuitability(Type type)
        {
            if (type?.GetTypeInfo().IsEnum == true)
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
            Type enumtype = Enum.GetUnderlyingType(type);
            bool result = BinarySerializer.Deserialize(reader, enumtype, out dynamic _value);
            if (result)
            {
                value = Enum.ToObject(type, _value);
            }
            else
            {
                value = null;
            }
            return result;
        }

        public bool GetLength(dynamic value, out int length)
        {
            Type enumtype = Enum.GetUnderlyingType(value.GetType());
            return BinarySerializer.GetLength(Convert.ChangeType(value, enumtype), out length);
        }

        public bool Serialize(BinaryWriter writer, dynamic value)
        {
            Type enumtype = Enum.GetUnderlyingType(value.GetType());
            return BinarySerializer.Serialize(writer, Convert.ChangeType(value, enumtype));
        }
    }
}
