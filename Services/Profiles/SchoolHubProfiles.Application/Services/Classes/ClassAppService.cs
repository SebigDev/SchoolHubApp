using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolHub.Core.Extensions;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Classes;
using SchoolHubProfiles.Core.Models.Classes;
using SchoolHubProfiles.Core.Models.Mapping;
using SchoolHubProfiles.Core.Models.Staffs;

namespace SchoolHubProfiles.Application.Services.Classes
{
    public class ClassAppService : IClassAppService
    {
        private readonly SchoolHubDbContext _schoolHubDbContext;

        public ClassAppService(SchoolHubDbContext schoolHubDbContext)
        {
            _schoolHubDbContext = schoolHubDbContext;
        }


        public async Task<long> InsertClass(CreateClassDto model)
        {
            ClassName className;
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            className = new ClassName
            {
                ClassCode = model.ClassCode,
                Name = model.Name,
                Category = model.Category,
                CreatedOn = DateTime.UtcNow,
            };
            await _schoolHubDbContext.ClassName.AddAsync(className);
            await _schoolHubDbContext.SaveChangesAsync();
            return className.Id;
        }


        public async Task<long> AssignClassesToStaff(long staffId, long classId)
        {
            var staff = new Staff();
            var nClass = new ClassName();
            var staffClass = new StaffClassMap
            {
                StaffId = staffId,
                ClassId = classId
            };
            await _schoolHubDbContext.StaffClassMap.AddAsync(staffClass);
            await _schoolHubDbContext.SaveChangesAsync();
            return staffClass.Id;
        }

        public async Task<StaffClassAssIgnedResponse> RetrieveClassesForStaff(long staffId)
        {
            StaffClassAssIgnedResponse response;
            var classDto = new List<ClassDto>();
            var retrieveClassForStaff = await _schoolHubDbContext.StaffClassMap
                                .Where(x => x.StaffId == staffId).ToListAsync();
            if(retrieveClassForStaff.Count() > 0)
            {
                foreach(var classStaff in retrieveClassForStaff)
                {
                    var nClass = await RetrieveClassById(classStaff.ClassId);
                    classDto.Add(nClass);
                }
            }
            response = new StaffClassAssIgnedResponse
            {
                StaffId = staffId,
                Classes = classDto
            };
            return response;

        }

        public async Task<bool> DeleteClass(long Id)
        {
            if (Id < 1)
                throw new ArgumentNullException(nameof(Id));
            var retrieveClass = await _schoolHubDbContext.ClassName.FirstOrDefaultAsync(s => s.Id == Id);
            if(retrieveClass != null)
            {
                 _schoolHubDbContext.Remove(retrieveClass);
                await _schoolHubDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<IEnumerable<ClassDto>> RetrieveAllClasses()
        {
            var allClasses = new List<ClassDto>();
            var retrieveClasses = await _schoolHubDbContext.ClassName.ToListAsync();
            allClasses.AddRange(retrieveClasses.OrderBy(c =>c.Id).Select(s => new ClassDto()
            {
                Id = s.Id,
                ClassCode = s.ClassCode,
                Name = s.Name,
                Category = s.Category.GetDescription(),
                CreatedOn = s.CreatedOn
            }));
            return allClasses;
        }

        public async Task<ClassDto> RetrieveClassById(long Id)
        {
            if (Id < 1)
                throw new ArgumentNullException(nameof(Id));

            var aClass = new ClassDto();
            var retrieveClass = await _schoolHubDbContext.ClassName.FirstOrDefaultAsync(s => s.Id == Id);
            aClass = new ClassDto
            {
                Id = retrieveClass.Id,
                ClassCode = retrieveClass.ClassCode,
                Name = retrieveClass.Name,
                CreatedOn = retrieveClass.CreatedOn
            };
            return aClass;
        }

        public async Task<bool> UpdateClass(ClassDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            var retrieveForUpdate = await _schoolHubDbContext.ClassName
                                         .Where(s => s.Id == model.Id).FirstOrDefaultAsync();
            if(retrieveForUpdate != null)
            {
                retrieveForUpdate.Id = model.Id;
                retrieveForUpdate.ClassCode = model.ClassCode;
                retrieveForUpdate.Name = model.Name;
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
        // ~ClassAppService() {
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
