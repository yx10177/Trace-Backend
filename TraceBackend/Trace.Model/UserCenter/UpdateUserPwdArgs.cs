using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class UpdateUserPwdArgs : RequestBase
    {
        public string Password { get; set; }
    }
}
