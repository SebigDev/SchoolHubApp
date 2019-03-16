using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHub.Notification.Services.Templates
{
    public class TemplateService : ITemplateService
    {
        public async Task<string> RetrieveTemplate(int notification)
        {
            var template = string.Empty;
           if(notification == 1)
            {
               template = "<div style=\"padding: 5px; \"><b>Dear {username}</b>,<br/><br/> Your registration was successful. <br/><br/> Below are your Credentials" +
               "<br/> Email Address: <b>{emailAddress}</b><br/>Password: <b>{password}</b><br/><br/>Thanks for choosing <b>SchoolHub</b></div>";
            }
           else if(notification == 2)
            {
                template = "<div style=\"padding: 5px; \"><b>Dear {username}</b>,<br/><br/> Your password change was successful.<br/><br/>" +
                    "You will have to Login to update your profile<br/><br/>Thanks for choosing <b>SchoolHub</b></div>";
            }
           else if(notification == 3)
            {
                template = "<div style=\"padding: 5px; \"><b>Dear {username}</b>,<br/><br/> Your reset was successful. <br/><br/> Below is your new Password" +
              "<br/>Password: <b>{password}</b><br/><br/>Thanks for choosing <b>SchoolHub</b></div>";
            }
            return await Task.FromResult(template);
        }
    }
}
