using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Serializator
{
    /// <summary>
    /// Атрибут, позволяющий избежать сериализации конкретного поля или свойства.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class SerializationEscapeAttribute : Attribute
    {
    }
}
