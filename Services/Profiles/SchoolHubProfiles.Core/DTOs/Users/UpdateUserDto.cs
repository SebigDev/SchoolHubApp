using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Users
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        
        public int UserType { get; set; }

        public bool IsUpdated { get; set; }

        public bool IsEmailConfirmed { get; set; }
        
        public DateTime? UpdatedOn { get; set; }
    }
}
