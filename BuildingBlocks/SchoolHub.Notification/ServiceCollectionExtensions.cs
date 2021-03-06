﻿using Microsoft.Extensions.DependencyInjection;
using SchoolHub.Notification.Services.MailService;
using SchoolHub.Notification.Services.Process;
using SchoolHub.Notification.Services.Templates;

namespace SchoolHub.Notification
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSchoolHubNotification(this IServiceCollection services)
        {
            services.AddTransient<ITemplateService, TemplateService>();
            services.AddTransient<IMailAppService, MailAppService>();
            services.AddTransient<INotificationProcessor, NotificationProcessor>();
            return services;
        }
    }
}
