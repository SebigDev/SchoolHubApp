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
        Task<long> InsertUser(CreateUserDto model);
        Task<string> Login(string username = null, string emailAddress = null, string password = null);
       // Task<bool> UpdateUser(UpdateUserDto model);
        //Task<bool> UpdateStaff(UpdateStaffDto model);
        Task<bool> DeleteUser(long Id);
        Task<UserDto> RetrieveUser(long Id);
        Task<IEnumerable<UserDto>> RetrieveAllUsers();
        Task<bool> UserExists(string emailAddress);
        Task<bool> GetUserByEmailAddress(string email);
    }
}
