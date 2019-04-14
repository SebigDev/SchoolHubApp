using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolHub.Core.Enums;
using SchoolHub.Core.Extensions;
using SchoolHub.Notification.Services.Process;
using SchoolHubProfiles.Application.Services.Users;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Auth;

namespace SchoolHubProfiles.Application.Services.Authentication
{
    public class AuthenticationAppService : IAuthenticationAppService
    {
        private readonly IUserAppService _userAppService;
        private readonly SchoolHubDbContext _schoolHubDbContext;
        private readonly INotificationProcessor _notificationProcessor;

        public AuthenticationAppService(IUserAppService userAppService, SchoolHubDbContext schoolHubDbContext,
            INotificationProcessor notificationProcessor
            )
        {
            _userAppService = userAppService;
            _schoolHubDbContext = schoolHubDbContext;
            _notificationProcessor = notificationProcessor;
        }
        public async Task<AuthResponse> ChangePassword(string email, string oldPassword, string newPassword)
        {

            var checkUser = await _schoolHubDbContext.User
                                    .Where(u => u.EmailAddress == email && u.Password == oldPassword && u.IsEmailConfirmed == false)
                                    .FirstOrDefaultAsync();

            CreatePasswordEncrypt(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            if (checkUser != null)
            {
                checkUser.Password = newPassword;
                checkUser.PasswordHash = passwordHash;
                checkUser.PasswordSalt = passwordSalt;
                checkUser.IsEmailConfirmed = true;
            };
            _schoolHubDbContext.Entry(checkUser).State = EntityState.Modified;
            await _schoolHubDbContext.SaveChangesAsync();

            var response = new AuthResponse
            {
                Status = true,
                Success = AuthResponseEnum.Yes.GetDescription()
            };

            //TODO: Send Email to User
            #region New Password Change Notification
            const int type = (int)NotificationType.PasswordChange;
            await _notificationProcessor.ProcessNotificationAsync(checkUser, type);
            #endregion
            return response;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var getUser = await _schoolHubDbContext.User.Where(x => x.Id == request.UserId && x.IsEmailConfirmed == true).FirstOrDefaultAsync();
            if(getUser != null)
            {
                getUser.Password = request.Password;
            }
            _schoolHubDbContext.Entry(getUser).State = EntityState.Modified;
            await _schoolHubDbContext.SaveChangesAsync();

            //TODO: Send Email to User
            #region New Password Reset Notification
            const int type = (int)NotificationType.PasswordReset;
            await _notificationProcessor.ProcessNotificationAsync(getUser, type);
            #endregion

            return true;
        }

        #region Helpers
        /// <summary>
        /// Creates an encrypted password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordEncrypt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AuthenticationAppService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
          // Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
            Dispose(false);
        }
        #endregion
    }
}
