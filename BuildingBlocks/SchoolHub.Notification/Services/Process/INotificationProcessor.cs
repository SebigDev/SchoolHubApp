using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHub.Notification.Services.Process
{
    public interface INotificationProcessor
    {
        /// <summary>
        /// Processes the Notification based on the response and notification type
        /// </summary>
        /// <param name="response"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<bool> ProcessNotificationAsync(dynamic response, int type);
    }
}
