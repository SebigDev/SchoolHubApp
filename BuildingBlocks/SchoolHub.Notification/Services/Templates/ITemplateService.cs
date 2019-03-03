using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHub.Notification.Services.Templates
{
    public interface ITemplateService
    {
        Task<string> RetrieveTemplate(int notification);
    }
}
