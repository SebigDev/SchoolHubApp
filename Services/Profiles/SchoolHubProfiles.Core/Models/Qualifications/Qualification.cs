﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Qualifications
{
    public class Qualification
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public string Certficate { get; set; }
        public DateTime DateObtained { get; set; }
        public long StaffId { get; set; }

    }
}
