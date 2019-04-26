using Microsoft.EntityFrameworkCore;
using SchoolHub.Core.Enums;
using SchoolHub.Core.Extensions;
using SchoolHubProfiles.Application.Services.Mapping;
using SchoolHubProfiles.Application.Services.Users;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Staffs;
using SchoolHubProfiles.Core.Models.Mapping;
using SchoolHubProfiles.Core.Models.Qualifications;
using SchoolHubProfiles.Core.Models.Staffs;
using SchoolHubProfiles.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var checkStaff = await RetriveStaffByUserId(model.UserId);
            if(checkStaff != null)
            {
                return 0;
            }
            else
            {
                staff = new Staff
                {
                    UserId = user.Id,
                    Firstname = model.Firstname,
                    Middlename = model.Middlename,
                    Lastname = model.Lastname,
                    DateOfBirth = model.DateOfBirth,
                    DateEmployed = model.DateEmployed,
                    Gender =  model.Gender,
                    UserType = model.UserType,
                    IsUpdate = true,
                    IsActive = true,
                };
                await _schoolHubDbContext.Staff.AddAsync(staff);
                await _schoolHubDbContext.SaveChangesAsync();

                var nUser = await _schoolHubDbContext.User.Where(s => s.Id == user.Id).FirstOrDefaultAsync();
                if(nUser != null)
                {
                    nUser.IsUpdated = true;
                };
                 _schoolHubDbContext.Entry(nUser).State = EntityState.Modified;
                await _schoolHubDbContext.SaveChangesAsync();
            }

            //INsert into UserStaffMap
            await _mappingService.MapStaffUser(staff.UserId, staff.Id);
            return await Task.FromResult(staff.Id);
        }

        public async Task<bool> UpdateStaff(StaffDto update)
        {
            var retrieveStaff = await _schoolHubDbContext.Staff.FirstOrDefaultAsync(x =>x.Id == update.Id);
            if(retrieveStaff != null)
            {
                retrieveStaff.Id = update.Id;
                retrieveStaff.Firstname = update.Firstname;
                retrieveStaff.Middlename = update.Middlename;
                retrieveStaff.Lastname = update.Lastname;
                retrieveStaff.IsUpdate = true;
            }
            _schoolHubDbContext.Entry(retrieveStaff).State = EntityState.Modified;
            await _schoolHubDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StaffDto>> RetrieveAllStaffs()
        {
            var allStaffs = await _schoolHubDbContext.Staff.ToListAsync();
            var staffDto = new List<StaffDto>();

            staffDto.AddRange(allStaffs.OrderBy(x => x.DateEmployed).Select(s => new StaffDto()
            {
                Id = s.Id,
                UserId = s.UserId,
                Firstname = s.Firstname,
                Middlename = s.Middlename,
                Lastname = s.Lastname,
                Age = s.Age,
                DateEmployed = s.DateEmployed,
                Gender = s.Gender.GetDescription(),
                IsActive = s.IsActive,
                IsUpdate = s.IsUpdate,
                Image = s.Image,
            }));
            return staffDto;
        }

        public async Task<IEnumerable<StaffDto>> RetrieveStaffByUserType(int type)
        {
            var staffs = await _schoolHubDbContext.Staff.Where(x => (int)(x.UserType) == type).ToListAsync();
            var staffDto = new List<StaffDto>();
            staffDto.AddRange(staffs.OrderBy(x => x.DateEmployed).Select(s => new StaffDto()
            {
                UserId = s.UserId,
                Firstname = s.Firstname,
                Middlename = s.Middlename,
                Lastname = s.Lastname,
                Age = s.Age,
                DateEmployed = s.DateEmployed,
                Gender = s.Gender.GetDescription(),
                UserType = s.UserType.GetDescription(),
                IsActive = s.IsActive,
                IsUpdate = s.IsUpdate,
                Image = s.Image,
            }));
            return staffDto;
        }

        public async Task<StaffQualificationResponse> RetriveStaffById(long Id)
        {
            StaffDto staffDto;

            var staff = await _schoolHubDbContext.Staff.FirstOrDefaultAsync(x => x.Id == Id);
            if (staff == null)
                return null;
            staffDto = new StaffDto
            {
                Id = staff.Id,
                UserId = staff.UserId,
                Firstname = staff.Firstname,
                Middlename = staff.Middlename,
                Lastname = staff.Lastname,
                Age = staff.Age,
                DateEmployed = staff.DateEmployed,
                Gender = staff.Gender.GetDescription(),
                IsActive = staff.IsActive,
                IsUpdate = staff.IsUpdate,
                UserType = staff.UserType.GetDescription(),
                Image = staff.Image,
            };
            var qualDto = await GetQualificationsByStaffId(Id);
            var response = new StaffQualificationResponse
            {
                Staff = staffDto,
                Qualification = qualDto.ToList()
            };

            return response;

        }

        public async Task SavePicture(StaffDto staffDto)
        {
            var staff = await _schoolHubDbContext.Staff.FirstOrDefaultAsync(x => x.Id == staffDto.Id);
            if(staff != null)
            {
                staff.Id = staffDto.Id;
                staff.Image = staffDto.Image;
            }
            _schoolHubDbContext.Entry(staff).State = EntityState.Modified;
            await _schoolHubDbContext.SaveChangesAsync();
        }

        public async Task<StaffQualificationResponse> RetriveStaffByUserId(long userId)
        {
            StaffDto staffDto;

            var staff = await _schoolHubDbContext.Staff.FirstOrDefaultAsync(x => x.UserId == userId);
            if (staff == null)
                return null;
            staffDto = new StaffDto
            {
                Id = staff.Id,
                UserId = staff.UserId,
                Firstname = staff.Firstname,
                Middlename = staff.Middlename,
                Lastname = staff.Lastname,
                Age = staff.Age,
                DateEmployed = staff.DateEmployed,
                Gender = staff.Gender.GetDescription(),
                IsActive = staff.IsActive,
                IsUpdate = staff.IsUpdate,
                UserType = staff.UserType.GetDescription(),
                Image = staff.Image,
            };
            var qualDto = await GetQualificationsByStaffId(staff.Id);
            var response = new StaffQualificationResponse
            {
                Staff = staffDto,
                Qualification = qualDto.ToList()
            };

            return response;

        }


        public async Task<int> AddQualification(AddQualificationDto model)
        {
            Qualification qualification;
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            qualification = new Qualification
            {
                Institution = model.Institution,
                Certficate = model.Certficate,
                DateObtained = model.DateObtained
            };
            await _schoolHubDbContext.Qualification.AddAsync(qualification);
            await _schoolHubDbContext.SaveChangesAsync();

            //TODO: Add To Mapp
            var map = new StaffQualificationMap
            {
                Id = qualification.Id,
                StaffId = qualification.StaffId
            };
            await _schoolHubDbContext.StaffQualificationMap.AddAsync(map);
            return qualification.Id;
        }

        public async Task<IEnumerable<QualificationDto>> GetQualificationsByStaffId(long staffId)
        {
            if (staffId < 1)
                throw new ArgumentNullException(nameof(staffId));
            var staffQuals = new List<QualificationDto>();
            var quals = await _schoolHubDbContext.Qualification.Where(x => x.StaffId == staffId).ToListAsync();
            staffQuals.AddRange(quals.OrderByDescending(s => s.DateObtained).Select(x => new QualificationDto()
            {
                Institution = x.Institution,
                Certficate = x.Certficate,
                DateObtained = x.DateObtained
            }));
            return staffQuals;
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
        //~StaffAppService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
           //Dispose(false);
       //  }

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
