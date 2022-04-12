
using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class ResponseBase<T>
    {
        public T Entries { get; set; }
        public string Message { get; set; } = string.Empty;
        public EnumStatusCode StatusCode { get => string.IsNullOrEmpty(Message) ? EnumStatusCode.Success : EnumStatusCode.Fail; }
    }
    public enum EnumStatusCode 
    {
        Success = 0,
        Fail = 1,
    }
}
