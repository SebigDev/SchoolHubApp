using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Staffs
{
   public class StaffQualificationResponse
    {
        public StaffDto Staff { get; set; }
        public List<QualificationDto> Qualification { get; set; }
    }
}
