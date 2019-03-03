using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.Models.Mapping;
using System;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Mapping
{
    public class MappingService : IMappingService
    {
        private readonly SchoolHubDbContext _schoolHubDbContext;
        public MappingService(SchoolHubDbContext schoolHubDbContext)
        {
            _schoolHubDbContext = schoolHubDbContext;
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
