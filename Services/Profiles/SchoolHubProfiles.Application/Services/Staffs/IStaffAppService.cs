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
        Task<long> InsertStaff(CreateStaffDto model);
        Task<StaffQualificationResponse> RetriveStaffById(long Id);
        Task<IEnumerable<StaffDto>> RetrieveStaffByStaffByUserType(string type);
        Task<IEnumerable<StaffDto>> RetrieveAllStaffs();

        Task<int> AddQualification(AddQualificationDto model);

        Task<IEnumerable<QualificationDto>> GetQualificationsByStaffId(long staffId);

    }
}
