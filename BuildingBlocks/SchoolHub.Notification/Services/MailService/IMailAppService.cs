using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHub.Notification.Services.MailService
{
    public interface IMailAppService
    {
        Task<bool> SendMail(string title, string emailDestination, string sender, string message);
    }
}
