using SchoolHubProfiles.Core.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Authentication
{
    public interface IAuthenticationAppService : IDisposable
    {
        Task<AuthResponse> ChangePassword(string email, string password);
        Task<bool> ResetPassword(ResetPasswordRequest request);
    }
}
