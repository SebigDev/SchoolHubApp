using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Staffs
{
    public class AddQualificationDto
    {
        public string Institution { get; set; }
        public string Certficate { get; set; }
        public DateTime DateObtained { get; set; }
    }
}
