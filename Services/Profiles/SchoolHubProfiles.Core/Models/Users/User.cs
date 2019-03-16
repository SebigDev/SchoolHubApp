using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.Models.Users
{
    public class User
    {
        public User()
        {
            RegisteredOn = DateTime.UtcNow;
           
        }
        public long Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? IsAdmin { get; set; }
        public int UserType { get; set; }

        public bool IsUpdated { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
