using Microsoft.EntityFrameworkCore;
using SchoolHubProfiles.Application.Services.Subjects;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Subjects;
using SchoolHubProfiles.Core.Models.Mapping;
using SchoolHubProfiles.Core.Models.Subjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Mapping
{
    public class MappingService : IMappingService
    {
        private readonly SchoolHubDbContext _schoolHubDbContext;
        private readonly ISubjectAppService _subjectAppService;

        public MappingService(SchoolHubDbContext schoolHubDbContext, ISubjectAppService subjectAppService)
        {
            _schoolHubDbContext = schoolHubDbContext;
            _subjectAppService = subjectAppService;
        }
        public async Task<long> MapStaffUser(long userId, long staffId)
        {
            var nStaffMap = new StaffUserMap
            {
                UserId = userId,
                StaffId = staffId,
                DateMapped = DateTime.UtcNow
            };
            await _schoolHubDbContext.StaffUserMap.AddAsync(nStaffMap);
            await _schoolHubDbContext.SaveChangesAsync();
            return nStaffMap.Id;
        }

        public async Task<long> MappSubjectClass(long classId, CreateSubjectDto createSubjectDto)
        {
            ClassSubjectMap classSubjectMap;
            var retrieveClass = await _schoolHubDbContext.ClassName.FirstOrDefaultAsync(c => c.Id == classId);
            if(retrieveClass != null)
            {
                var createSubject = await _subjectAppService.InsertSubject(createSubjectDto);
                classSubjectMap = new ClassSubjectMap
                {
                    ClassId = classId,
                    SubjectId = createSubject,
                    MappedOn = DateTime.UtcNow
                };

                await _schoolHubDbContext.ClassSubjectMap.AddAsync(classSubjectMap);
                await _schoolHubDbContext.SaveChangesAsync();
                return classSubjectMap.Id;
            }
            return 0;
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
        // ~MappingService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

      
        #endregion
    }
}
