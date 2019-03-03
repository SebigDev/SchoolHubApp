using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Mapping
{
    public class StaffUserMap
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long StaffId { get; set; }
        public DateTime DateMapped { get; set; }
    }
}
