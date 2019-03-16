using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Students
{
   public class StudentClassResponse
    {
        public long ClassId { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}
