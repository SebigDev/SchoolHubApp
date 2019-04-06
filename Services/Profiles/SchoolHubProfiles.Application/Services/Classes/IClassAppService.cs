using SchoolHubProfiles.Core.DTOs.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Classes
{
    public interface IClassAppService : IDisposable
    {
        /// <summary>
        /// Creates a Class Object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> InsertClass(CreateClassDto model);
        /// <summary>
        /// Deletes a Class Object
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> DeleteClass(long Id);
        /// <summary>
        /// Updates a Class Object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateClass(ClassDto model);
        /// <summary>
        /// Retrieves a Class By Class Identity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ClassDto> RetrieveClassById(long Id);
        /// <summary>
        /// Retrieves All Classes
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ClassDto>> RetrieveAllClasses();

        Task<long> AssignClassesToStaff(long staffId, long classId);

        Task<StaffClassAssIgnedResponse> RetrieveClassesForStaff(long staffId);
        Task<IEnumerable<ClassDto>> RetriveUnAssignedClasses(long staffId);

    }
}
