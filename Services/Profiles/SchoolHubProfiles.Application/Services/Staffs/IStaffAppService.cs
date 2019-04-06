using SchoolHubProfiles.Core.DTOs.Staffs;
using SchoolHubProfiles.Core.Models.Qualifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Staffs
{
   public interface IStaffAppService : IDisposable
    {
        /// <summary>
        /// Inserts a Staff
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> InsertStaff(CreateStaffDto model);
        /// <summary>
        /// Updates the staff object
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdateStaff(StaffDto update);
        /// <summary>
        /// Retrieves a Staff
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<StaffQualificationResponse> RetriveStaffById(long Id);
        /// <summary>
        /// Retrieves a Staff
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<StaffQualificationResponse> RetriveStaffByUserId(long userId);
        /// <summary>
        /// Retieves staff by user type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<IEnumerable<StaffDto>> RetrieveStaffByUserType(int type);
        /// <summary>
        /// Retrieves All staffs
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StaffDto>> RetrieveAllStaffs();

        /// <summary>
        /// Creates a Qualification for a staff
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddQualification(AddQualificationDto model);

        /// <summary>
        /// Retrieves a Qualification by Staff Identity
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        Task<IEnumerable<QualificationDto>> GetQualificationsByStaffId(long staffId);

        Task SavePicture(StaffDto staffDto);


    }
}
