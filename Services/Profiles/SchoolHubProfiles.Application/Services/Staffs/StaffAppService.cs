using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolHubProfiles.Application.Services.Mapping;
using SchoolHubProfiles.Application.Services.Users;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Staffs;
using SchoolHubProfiles.Core.Models.Staffs;

namespace SchoolHubProfiles.Application.Services.Staffs
{
    public class StaffAppService : IStaffAppService
    {
        #region PPties
        private readonly SchoolHubDbContext _schoolHubDbContext;
        private readonly IMappingService _mappingService;
        private readonly IUserAppService _userAppService;
        #endregion

        #region Ctor
        public StaffAppService(SchoolHubDbContext schoolHubDbContext,
            IMappingService mappingService, IUserAppService userAppService)
        {
            _schoolHubDbContext = schoolHubDbContext;
            _mappingService = mappingService;
            _userAppService = userAppService;
        }
        #endregion

        public async Task<long> InsertStaff(CreateStaffDto model)
        {
            Staff staff;

            if (model == null)
                throw new ArgumentNullException(nameof(model));
            var user = await _userAppService.RetrieveUser(model.UserId);
            staff = new Staff
            {
                UserId = user.Id,
                Firstname = model.Firstname,
                Middlename = model.Middlename,
                Lastname = model.Lastname,
                DateOfBirth = model.DateOfBirth,
                DateEmployed = model.DateEmployed,
                Gender = (int)model.Gender,
                UserType = (int)model.UserType,
            };
            await _schoolHubDbContext.Staff.AddAsync(staff);
            await _schoolHubDbContext.SaveChangesAsync();

            //INsert into UserStaffMap
            await _mappingService.MapStaffUser(staff.UserId, staff.Id);
            return await Task.FromResult(staff.Id);
        }

        public async Task<IEnumerable<StaffDto>> RetrieveAllStaffs()
        {
            var allStaffs = await _schoolHubDbContext.Staff.ToListAsync();
            var staffDto = new List<StaffDto>();

            staffDto.AddRange(allStaffs.OrderBy(x => x.DateEmployed).Select(s => new StaffDto()
            {
                UserId = s.Id,
                Firstname = s.Firstname,
                Middlename = s.Middlename,
                Lastname = s.Lastname,
                Age = s.Age,
                DateEmployed = s.DateEmployed,
                Gender = (int)s.Gender,
                IsActive = s.IsActive,
                IsUpdate = s.IsUpdate,
            }));
            return staffDto;
        }

        public async Task<IEnumerable<StaffDto>> RetrieveStaffByStaffByUserType(int type)
        {
            var staffs = await _schoolHubDbContext.Staff.Where(x => x.UserType == type).ToListAsync();
            var staffDto = new List<StaffDto>();
            staffDto.AddRange(staffs.OrderBy(x => x.DateEmployed).Select(s => new StaffDto()
            {
                UserId = s.Id,
                Firstname = s.Firstname,
                Middlename = s.Middlename,
                Lastname = s.Lastname,
                Age = s.Age,
                DateEmployed = s.DateEmployed,
                Gender = (int)s.Gender,
                IsActive = s.IsActive,
                IsUpdate = s.IsUpdate,
            }));
            return staffDto;
        }

        public async Task<StaffDto> RetriveStaffById(long Id)
        {
            StaffDto staffDto;

            var staff = await _schoolHubDbContext.Staff.FirstOrDefaultAsync(x => x.Id == Id);
            if (staff == null)
                return null;
            staffDto = new StaffDto
            {
                UserId = staff.Id,
                Firstname = staff.Firstname,
                Middlename = staff.Middlename,
                Lastname = staff.Lastname,
                Age = staff.Age,
                DateEmployed = staff.DateEmployed,
                Gender = (int)staff.Gender,
                IsActive = staff.IsActive,
                IsUpdate = staff.IsUpdate,
            };
            return staffDto;

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
        // ~StaffAppService() {
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
