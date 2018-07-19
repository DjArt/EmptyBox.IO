using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Serializator
{
    public interface ISerializationScenario
    {
        string Name { get; }
        TOut Wrap<TIn, TOut>(TIn x, string @case);
        TOut Unwrap<TIn, TOut>(TIn x, string @case);
    }
}
