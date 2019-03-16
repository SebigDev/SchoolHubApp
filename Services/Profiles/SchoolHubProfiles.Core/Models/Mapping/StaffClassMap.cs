using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Mapping
{
    public class StaffClassMap
    {
        public long Id { get; set; }
        public long StaffId { get; set; }
        public long ClassId { get; set; }
    }
}
