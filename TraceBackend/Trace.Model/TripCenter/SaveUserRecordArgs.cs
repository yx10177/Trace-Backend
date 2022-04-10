using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class SaveUserRecordArgs : RequestBase
    {
        public int UserRecordId { get; set; } = 0;
        public int TripId { get; set; }
        public string OccurDt { get; set; }
        public string Location { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }
}
