using SchoolHubProfiles.Core.DTOs.Auth;
using SchoolHubProfiles.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Authentication
{
    public interface IAuthenticationAppService : IDisposable
    {
        /// <summary>
        /// Password Change
        /// </summary>
        /// <param name="email"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<AuthResponse> ChangePassword(string email, string oldPassword, string newPassword);
        /// <summary>
        /// Resets a user password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(ResetPasswordRequest request);

        Task<User> LogOut(long id);
    }
}
