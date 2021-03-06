﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Staffs
{
    public class StaffDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        
        public DateTime DateEmployed { get; set; }

        public bool? IsActive { get; set; }

        public bool IsUpdate { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public string UserType { get; set; }

        public string ImagePath { get; set; }
    }
}
                