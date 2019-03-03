using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHub.Notification.Services.Process
{
    public interface INotificationProcessor
    {
        Task<bool> ProcessNotificationAsync(dynamic response, int type);
    }
}
