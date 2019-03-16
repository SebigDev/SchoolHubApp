using SchoolHubProfiles.Core.DTOs.Students;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Students
{
    public interface IStudentAppService : IDisposable
    {
        /// <summary>
        /// Inserts a Student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> InsertStudent(long classId,CreateStudentDto model);
   
        /// <summary>
        /// Retieves Student by class Identity
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<StudentClassResponse> RetrieveStudentsByClassID(long classId);

        /// <summary>
        /// Retrieves All Students
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StudentDto>> RetrieveAllStudents();

    }
}
