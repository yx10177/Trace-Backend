using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Trace.Model
{
    public class RequestBase
    {
        [JsonIgnore]
        public JWTPayload Payload { get; set; }
    }
}
