using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SchoolHub.Notification.Services.MailService;
using SchoolHub.Notification.Services.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHub.Notification.Services.Process
{
    public class NotificationProcessor : INotificationProcessor
    {
        private readonly ITemplateService _templateService;
        private readonly IMailAppService _mailAppService;
        private MailSettings _settings;

        private readonly IHostingEnvironment _env;

        public NotificationProcessor(ITemplateService templateService, IMailAppService mailAppService,
            IOptions<MailSettings> options, IHostingEnvironment env)
        {
            _templateService = templateService;
            _mailAppService = mailAppService;
            _settings = options.Value;
            _env = env;
        }


        public async Task<bool> ProcessNotificationAsync(dynamic response, int type)
        {
            var templateResponse = string.Empty;
            var messageTitle = string.Empty;

            var template = await _templateService.RetrieveTemplate(type);
            
            if(type == 1)
            {
                messageTitle = "Registration";
                templateResponse = template.Replace("{username}", $"{response.Username}")
                                           .Replace("{emailAddress}", $"{response.EmailAddress}")
                                           .Replace("{password}", $"{response.Password}");
            }
            var sendToMailService = await _mailAppService.SendMail(messageTitle, response.EmailAddress, _settings.Username, templateResponse);

            return await Task.FromResult(true);
        }
    }
}
