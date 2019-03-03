using SchoolHubProfiles.Core.DTOs.Staffs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Staffs
{
   public interface IStaffAppService : IDisposable
    {
        Task<long> InsertStaff(CreateStaffDto model);
        Task<StaffDto> RetriveStaffById(long Id);
        Task<IEnumerable<StaffDto>> RetrieveStaffByStaffByUserType(int type);
        Task<IEnumerable<StaffDto>> RetrieveAllStaffs();

    }
}
