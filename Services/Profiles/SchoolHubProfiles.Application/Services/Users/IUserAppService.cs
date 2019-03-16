using SchoolHubProfiles.Core.DTOs.Staffs;
using SchoolHubProfiles.Core.DTOs.Users;
using SchoolHubProfiles.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Users
{
   public interface IUserAppService : IDisposable
    {
        /// <summary>
        /// Inserts a new User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> InsertUser(CreateUserDto model);
        /// <summary>
        /// Logs in a User 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="emailAddress"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Login(string username = null, string emailAddress = null, string password = null);
        /// <summary>
        /// Updates a User Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(UpdateUserDto model);
        //Task<bool> UpdateStaff(UpdateStaffDto model);
        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(long Id);
        /// <summary>
        /// Retrieves a User by Identity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<UserDto> RetrieveUser(long Id);
        /// <summary>
        /// Retrieves all Users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> RetrieveAllUsers();
        /// <summary>
        /// Checks if a user already exists
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<bool> UserExists(string emailAddress);
        /// <summary>
        /// Gets a User by user email Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserDto> GetUserByEmailAddress(string email);
        /// <summary>
        /// Generates Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GenerateToken(User user);
    }
}
