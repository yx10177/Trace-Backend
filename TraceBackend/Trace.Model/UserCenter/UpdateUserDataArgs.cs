using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class UpdateUserDataArgs : RequestBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
    }
}
