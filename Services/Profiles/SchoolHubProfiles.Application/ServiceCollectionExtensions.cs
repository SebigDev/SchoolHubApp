using Microsoft.Extensions.DependencyInjection;
using SchoolHubProfiles.Application.Services.Mapping;
using SchoolHubProfiles.Application.Services.Staffs;
using SchoolHubProfiles.Application.Services.Users;

namespace SchoolHubProfiles.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSchoolHubProfileApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IMappingService, MappingService>();
            services.AddTransient<IStaffAppService, StaffAppService>();
         

            return services;
        }
    }
}
