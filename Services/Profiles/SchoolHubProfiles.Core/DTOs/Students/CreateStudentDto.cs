using SchoolHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Students
{
    public class CreateStudentDto
    {
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
