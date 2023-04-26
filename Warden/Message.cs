using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden
{
    public class Massage
    {
        public string SmtpClient { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Pass { get; set; }

        public Massage(string smtp, string from, string to, string subject, string pass)
        {
            SmtpClient = smtp;
            From = from;
            To = to;
            Subject = subject;
            Pass = pass;
        }
    }
}
