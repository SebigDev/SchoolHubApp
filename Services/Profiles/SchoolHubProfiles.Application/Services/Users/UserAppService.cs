using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolHub.Core.Enums;
using SchoolHub.Notification.Services.Process;
using SchoolHubProfiles.Application.Services.Mapping;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Users;
using SchoolHubProfiles.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Users
{
    public class UserAppService : IUserAppService
    {
        #region PPties
        private readonly SchoolHubDbContext _schoolHubDbContext;
        private readonly INotificationProcessor _notificationProcess;
        private readonly IMappingService _mappingService;
        #endregion

        #region Ctor
        public UserAppService(SchoolHubDbContext schoolHubDbContext, 
            INotificationProcessor notificationProcessor,
            IMappingService mappingService)
        {
            _schoolHubDbContext = schoolHubDbContext;
            _notificationProcess = notificationProcessor;
            _mappingService = mappingService;
        }
        #endregion

        #region Action Methods
        public async Task<User> Login(string username = null, string emailAddress = null, string password = null)
        {
            var isUser = await _schoolHubDbContext.User.FirstOrDefaultAsync(x => x.Username == username || x.EmailAddress == emailAddress && x.Password == password);
            if (isUser == null)
                return null;
            if (!VerifyPasswordHash(password, isUser.PasswordHash, isUser.PasswordSalt))
                return null;
            return isUser;
        }

        public async Task<long> InsertUser(CreateUserDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           using(var txn = _schoolHubDbContext.Database.BeginTransaction())
           {
           
                    CreatePasswordEncrypt(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    var newUser = new User
                    {
                        Username = model.Username,
                        Password = model.Password,
                        EmailAddress = model.EmailAddress,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        IsEmailConfirmed = false,
                        IsUpdated = false,
                        IsAdmin = false,
                        UserType = (int)model.UserType,
                    };
                    if (model.UserType == UserTypeEnum.Admin)
                    {
                        newUser.IsAdmin = true;
                    }
                    await _schoolHubDbContext.User.AddAsync(newUser);
                    await _schoolHubDbContext.SaveChangesAsync();

                    //TODO: Send Email to User
                    #region New Registration Notification
                    const int type = (int)NotificationType.Registration;
                    await _notificationProcess.ProcessNotificationAsync(newUser, type);
                #endregion
                txn.Commit();
                return await Task.FromResult(newUser.Id);
                
           }
            
        }


        public async Task<bool> DeleteUser(long Id)
        {
            var delete = await _schoolHubDbContext.User.Where(s => s.Id == Id).FirstOrDefaultAsync();
            if (delete != null)
            {
                _schoolHubDbContext.Remove(delete);
                return true;
            }  
            return false;

        }

        public async Task<IEnumerable<UserDto>> RetrieveAllUsers()
        {
            var allUserDto = new List<UserDto>();
            var allUsers = await _schoolHubDbContext.User.ToListAsync();
 
                allUserDto.AddRange(allUsers.OrderBy(o => o.Id).Select(user => new UserDto()
                {
                    Id = user.Id,
                    EmailAddress = user.EmailAddress,
                    Username = user.Username,
                    IsAdmin = user.IsAdmin,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    IsUpdated = user.IsUpdated,
                    UserType = (UserTypeEnum)user.UserType
                }));
                return allUserDto;
        }

        public async Task<UserDto> RetrieveUser(long Id)
        {
            if (Id < 1)
                throw new ArgumentNullException(nameof(Id));
            UserDto userDto;
            var user = await _schoolHubDbContext.User.FirstOrDefaultAsync(x => x.Id == Id);
            if(user != null)
            {
                userDto = new UserDto
                {
                    Id = user.Id,
                    EmailAddress = user.EmailAddress,
                    Username = user.Username,
                    IsAdmin = user.IsAdmin,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    IsUpdated = user.IsUpdated,
                    UserType = (UserTypeEnum)user.UserType
                };
                return userDto;
            }
            return null;
        }

        public async Task<UserDto> GetUserByEmailAddress(string email)
        {
            UserDto userDto;
            var checkUser = await _schoolHubDbContext.User.Where(e => e.EmailAddress == email).FirstOrDefaultAsync();
            if (checkUser != null)
            {
                userDto = new UserDto
                {
                    Id = checkUser.Id,
                    EmailAddress = checkUser.EmailAddress,
                    IsEmailConfirmed = checkUser.IsEmailConfirmed,
                    Username = checkUser.Username,
                    IsAdmin = checkUser.IsAdmin,
                    IsUpdated = checkUser.IsUpdated,
                    UserType = (UserTypeEnum)checkUser.UserType,
                };
                return userDto;
            }
            return null;
        }

        public async Task<bool> UpdateUser(UpdateUserDto model)
        {
            var user = await _schoolHubDbContext.User.FindAsync(model.Id);
            if(user != null)
            {
                user.Username = model.Username;
                user.EmailAddress = model.EmailAddress;
                user.UserType = model.UserType;
                user.UpdatedOn = DateTime.UtcNow;
                user.Password = model.Password;
                user.IsUpdated = true;
            }
            _schoolHubDbContext.Entry(user).State = EntityState.Modified;
            await _schoolHubDbContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }


        #endregion

        #region Helper
        /// <summary>
        /// Generates a token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GenerateToken(User user)
        {
            //Generating Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Super Secret Key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                   new Claim(ClaimTypes.Name, user.EmailAddress)
                }),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return await Task.FromResult(tokenString);
        }


        //CREATION OF PASSWORD ENCRYPTION
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

        //Verify Password
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

            //CHECKING IF USER EXIST ALREADY
            public async Task<bool> UserExists(string emailAddress)
        {
            if (await _schoolHubDbContext.User
                        .AnyAsync(x => x.EmailAddress == emailAddress))
            {
                return true;
            }
            return false;
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
        // ~UserAppService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
