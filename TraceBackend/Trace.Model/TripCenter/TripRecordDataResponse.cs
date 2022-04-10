using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class TripRecordDataResponse
    {
        public int RecordId { get; set; }
        public int TripId { get; set; }
        public DateTime occureDt { get; set; }
        public string LocationTitle { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }
}
