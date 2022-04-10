using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class TripEventDataResponse 
    {
       
        public int TripId { get; set; }
        public string TripTitle { get; set; }
        public DateTime StartDt { get; set; }
        public DateTime EndDt { get; set; }
    }
}
