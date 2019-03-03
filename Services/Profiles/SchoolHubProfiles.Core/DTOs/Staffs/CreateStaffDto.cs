using SchoolHub.Core.Enums;
using SchoolHubProfiles.Core.Models.Staffs;
using SchoolHubProfiles.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Staffs
{
    public class CreateStaffDto
    {
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime DateEmployed { get; set; }

        public GenderEnum Gender { get; set; }

        public UserTypeEnum UserType { get; set; }
    }
}
