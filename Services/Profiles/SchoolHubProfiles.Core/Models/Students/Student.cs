using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Students
{
    public class Student
    {

        public Student()
        {
            IsActive = true;
            DateOfRegistration = DateTime.UtcNow;
            IsUpdate = false;

        }
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                var age = DateTime.Now.Subtract(DateOfBirth).Days / 365;
                return age;
            }
            set
            {
                value = Age;
            }
        }

        public bool? IsActive { get; set; }


        public bool IsUpdate { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
