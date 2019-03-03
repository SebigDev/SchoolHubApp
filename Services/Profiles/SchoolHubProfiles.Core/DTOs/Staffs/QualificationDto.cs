using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Staffs
{
    public class QualificationDto
    {
        public string Institution { get; set; }
        public string Certficate { get; set; }
        public DateTime DateObtained { get; set; }
        public long StaffId { get; set; }
    }
}
