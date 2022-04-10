using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class SaveTripEventArgs : RequestBase
    {
        public int TripId { get; set; }
        public string TripTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<int> FriendsId { get; set; }
    }
}
