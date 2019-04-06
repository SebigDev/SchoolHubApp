using SchoolHubProfiles.Core.DTOs.Subjects;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Subjects
{
    public interface ISubjectAppService
    {
        /// <summary>
        /// Creates a Subject Object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> InsertSubject(CreateSubjectDto model);
        /// <summary>
        /// Deletes a Subject Object
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> DeleteSubject(long Id);
        /// <summary>
        /// Updates a Subject Object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateSubject(SubjectDto model);
        /// <summary>
        /// Retrieves a Subject By Subject Identity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<SubjectDto> RetrieveSubjectById(long Id);
        /// <summary>
        /// Retrieves a Subject By Subject Identity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ClassSubjectResponse> RetrieveSubjectByClassId(long classId);
        /// <summary>
        /// Retrieves All Subjectes
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SubjectDto>> RetrieveAllSubjects();

        /// <summary>
        /// retrieves subjects bu Student Identity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentSubjectsResponse> RetrieveSubjectsByStudentId(long id);

    }
}
