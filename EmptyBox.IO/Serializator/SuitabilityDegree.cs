using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Serializator
{
    public enum SuitabilityDegree : byte
    {
        NotAssignable = 0,
        Assignable = 1,
        Equal = 2,
    }
}
