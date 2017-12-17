using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Serializator
{
    internal sealed class NotNullObject<T>
    {
        public T Value { get; set; }

        public NotNullObject(T value)
        {
            if (value != null)
            {
                Value = value;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
