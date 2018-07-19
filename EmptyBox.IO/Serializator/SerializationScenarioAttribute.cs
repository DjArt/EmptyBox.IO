using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Serializator
{
    /// <summary>
    /// Атрибут, позволяющий указать принадлежность объекта к определённому сценарию.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class SerializationScenarioAttribute : Attribute
    {
        public string Scenario { get; private set; }
        public string Case { get; set; }
        public Type WrappedType { get; private set; }

        public SerializationScenarioAttribute(string scenario, Type wrappedType)
        {
            Scenario = scenario;
            WrappedType = wrappedType;
        }
    }
}
