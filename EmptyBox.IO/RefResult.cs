using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO
{
    public struct RefResult<TResult, TStatus> where TResult : class
    {
        public static implicit operator TResult (RefResult<TResult, TStatus> x)
        {
            return x.Result;
        }

        public TResult Result { get; private set; }
        public TStatus Status { get; private set; }
        public Exception Exception { get; private set; }

        public RefResult(TResult result, TStatus status, Exception exception)
        {
            Result = result;
            Status = status;
            Exception = exception;
        }
    }
}
