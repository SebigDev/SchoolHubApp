using Microsoft.EntityFrameworkCore;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Subjects;
using SchoolHubProfiles.Core.Models.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Subjects
{
    public class SubjectAppService : ISubjectAppService
    {
        private readonly SchoolHubDbContext _schoolHubDbContext;

        public SubjectAppService(SchoolHubDbContext schoolHubDbContext)
        {
            _schoolHubDbContext = schoolHubDbContext;
        }


        public async Task<long> InsertSubject(CreateSubjectDto model)
        {
            Subject subject;
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            subject = new Subject
            {
                SubjectCode = model.SubjectCode,
                SubjectName = model.SubjectName,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = model.CreatedBy
            };
            await _schoolHubDbContext.Subject.AddAsync(subject);
            await _schoolHubDbContext.SaveChangesAsync();
            return subject.Id;
        }


        public async Task<bool> DeleteSubject(long Id)
        {
            if (Id < 1)
                throw new ArgumentNullException(nameof(Id));
            var retrieveSubject = await _schoolHubDbContext.Subject.FirstOrDefaultAsync(s => s.Id == Id);
            if (retrieveSubject != null)
            {
                _schoolHubDbContext.Remove(retrieveSubject);
                await _schoolHubDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<IEnumerable<SubjectDto>> RetrieveAllSubjects()
        {
            var allSubjects = new List<SubjectDto>();
            var retrieveSubjects = await _schoolHubDbContext.Subject.ToListAsync();
            allSubjects.AddRange(retrieveSubjects.OrderBy(c => c.Id).Select(s => new SubjectDto()
            {
                Id = s.Id,
                SubjectCode = s.SubjectCode,
                SubjectName = s.SubjectName,
                CreatedOn = s.CreatedOn
            }));
            return allSubjects;
        }

        public async Task<ClassSubjectResponse> RetrieveSubjectByClassId(long classId)
        {
            var allSubjects = new List<SubjectDto>();
            var nSubject = new SubjectDto();
            ClassSubjectResponse classSubjectResponse;

            var retrieveSubjects = await _schoolHubDbContext.ClassSubjectMap
                                       .Where(m => m.ClassId == classId).ToListAsync();
           if(retrieveSubjects.Count() > 0)
            {
                foreach (var sub in retrieveSubjects)
                {
                    nSubject = await RetrieveSubjectById(sub.SubjectId);

                    allSubjects.Add(nSubject);
                }
            }
            classSubjectResponse = new ClassSubjectResponse
            {
                ClassId = classId,
                Subjects = allSubjects
            };
            return classSubjectResponse;

        }

        public async Task<SubjectDto> RetrieveSubjectById(long Id)
        {
            if (Id < 1)
                throw new ArgumentNullException(nameof(Id));

            var aSubject = new SubjectDto();
            var retrieveSubject = await _schoolHubDbContext.Subject.FirstOrDefaultAsync(s => s.Id == Id);
            aSubject = new SubjectDto
            {
                Id = retrieveSubject.Id,
                SubjectCode = retrieveSubject.SubjectCode,
                SubjectName = retrieveSubject.SubjectName,
                CreatedOn = retrieveSubject.CreatedOn
            };
            return aSubject;
        }

        public async Task<bool> UpdateSubject(SubjectDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            var retrieveForUpdate = await _schoolHubDbContext.Subject
                                         .Where(s => s.Id == model.Id).FirstOrDefaultAsync();
            if (retrieveForUpdate != null)
            {
                retrieveForUpdate.Id = model.Id;
                retrieveForUpdate.SubjectCode = model.SubjectCode;
                retrieveForUpdate.SubjectName = model.SubjectName;
                retrieveForUpdate.CreatedOn = DateTime.UtcNow;

                return true;
            }
            return false;
        }

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
        // ~SubjectAppService() {
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
            Dispose(false);
        }
        #endregion
    }
}
