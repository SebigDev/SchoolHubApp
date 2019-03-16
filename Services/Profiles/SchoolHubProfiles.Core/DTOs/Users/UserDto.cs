using SchoolHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Users
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }

        public bool? IsEmailConfirmed { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsUpdated { get; set; }

        public UserTypeEnum UserType { get; set; }
    }
}
