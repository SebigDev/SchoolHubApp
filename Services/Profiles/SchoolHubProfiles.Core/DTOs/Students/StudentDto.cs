using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Students
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }


        public bool? IsActive { get; set; }


        public bool IsUpdate { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
