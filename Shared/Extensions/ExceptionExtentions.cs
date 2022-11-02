using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class ExceptionExtentions
    {
        public static Exception GetStackTraceRoot(this Exception exception)
        {
            if (exception.InnerException == null) return exception;
            return exception.InnerException;
        }
    }
}
