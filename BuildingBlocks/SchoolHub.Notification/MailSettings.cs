using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolHub.Notification
{
    public class MailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
