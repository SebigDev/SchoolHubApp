using Microsoft.Extensions.DependencyInjection;
using SchoolHubProfiles.Application.Services.Authentication;
using SchoolHubProfiles.Application.Services.Classes;
using SchoolHubProfiles.Application.Services.Mapping;
using SchoolHubProfiles.Application.Services.Payments;
using SchoolHubProfiles.Application.Services.Staffs;
using SchoolHubProfiles.Application.Services.Students;
using SchoolHubProfiles.Application.Services.Subjects;
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
            services.AddTransient<IAuthenticationAppService, AuthenticationAppService>();
            services.AddTransient<IClassAppService, ClassAppService>();
            services.AddTransient<ISubjectAppService, SubjectAppService>();
            services.AddTransient<IStudentAppService, StudentAppService>();
            services.AddTransient<IPaymentAppServices, PaymentAppServices>();
            return services;
        }
    }
}
