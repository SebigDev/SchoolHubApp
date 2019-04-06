using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Users
{
   public class UserLoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public long UserId { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
