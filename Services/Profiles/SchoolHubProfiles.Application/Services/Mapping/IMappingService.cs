using System;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Mapping
{
    public interface IMappingService : IDisposable
    {
        Task<long> MapStaffUser(long userId, long staffId);
    }
}
