using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class UserDataResponse
    {
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserPhotoPath { get; set; }
    }
}
