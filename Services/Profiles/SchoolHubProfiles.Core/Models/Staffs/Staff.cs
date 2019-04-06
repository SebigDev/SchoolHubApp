using SchoolHub.Core.Enums;
using System;
using System.Collections.Generic;

namespace SchoolHubProfiles.Core.Models.Staffs
{
    public class Staff
    {
        public Staff()
        {
            DateOfRegistration = DateTime.UtcNow;
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                var age = DateTime.Now.Subtract(DateOfBirth).Days/365;
                return age;
            }
            set
            {
                value = Age;
            }
        }
        public DateTime DateEmployed { get; set; }

        public bool? IsActive { get; set; }

        public bool IsUpdate { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime? DateUpdated { get; set; }

        public UserTypeEnum UserType { get; set; }

        public string Photo { get; set; }
    }

}
