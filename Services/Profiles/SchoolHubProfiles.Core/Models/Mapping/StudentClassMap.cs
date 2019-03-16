using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Mapping
{
    public class StudentClassMap
    {
        public StudentClassMap()
        {
            DateMapped = DateTime.UtcNow;
        }
        public long Id { get; set; }
        public long ClassId { get; set; }
        public long StudentId { get; set; }
        public DateTime DateMapped { get; set; }
    }
}
