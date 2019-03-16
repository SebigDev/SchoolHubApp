using System;
using System.Collections.Generic;
using System.Text;
using SchoolHub.Core.Enums;

namespace SchoolHubProfiles.Core.DTOs.Users
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }

        public bool? IsEmailConfirmed { get; set; }

        public UserTypeEnum UserType { get; set; }

    }
}
