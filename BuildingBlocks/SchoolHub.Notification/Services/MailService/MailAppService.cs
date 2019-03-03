using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHub.Notification.Services.MailService
{
    public class MailAppService : IMailAppService
    {
        private MailSettings _settings;
        public MailAppService(IOptions<MailSettings> options)
        {
            _settings = options.Value;
        }
        public async Task<bool> SendMail(string title, string emailDestination, string sender, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            
            smtpClient = new SmtpClient
            {
                Host = _settings.Host,
                Port = _settings.Port,
                EnableSsl = _settings.EnableSsl,
                Credentials = new NetworkCredential
                {
                    UserName = _settings.Username,
                    Password = _settings.Password
                }
            };
            var mailMessage = new MailMessage(sender, emailDestination)
            {
                Subject = title,
                Body = message,
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(mailMessage);
            return await Task.FromResult(true);
        }


    }
}
