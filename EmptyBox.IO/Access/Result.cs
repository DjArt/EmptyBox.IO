using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Access
{
    public struct Result<T>
    {
        public bool HasValue { get; set; }
        public T Value { get; set; }
        public Exception Exception { get; set; }

        public Result(T value)
        {
            Value = value;
            HasValue = true;
            Exception = null;
        }

        public Result(Exception exception)
        {
            HasValue = false;
            Value = default(T);
            Exception = exception;
        }

        public T GetValueOrDefault()
        {
            return Value;
        }
    }
}
