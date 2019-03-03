using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHubProfiles.Core.DTOs.Auth
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public long UserId { get; set; }
    }
}
