using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Warden
{
    class SendMail
    {
        private string smtp;
        private string from;
        private string subject;
        private string body;
        private string pass;

        public SendMail(string smtp, string from, string subject, string body, string pass)
        {
            this.smtp = smtp;
            this.from = from;
            this.subject = subject;
            this.body = body;
            this.pass = pass;
        }
        public void Send(string to)
        {
            // получаем SmtpClient
            SmtpClient smtpClient = GetSmtpClient();
            // получаем сообщение
            MailMessage mailMessage = GetMailMessage(to);

            // отправляем сообщение
            smtpClient.Send(mailMessage);
        }

        private MailMessage GetMailMessage(string to)
        {
            // создаём объект сообщения
            MailMessage mailMessage = new MailMessage();
            // указываем, от кого отсылается сообщение
            mailMessage.From = new MailAddress(from);
            // указываем, кому отправляется сообщение
            mailMessage.To.Add(new MailAddress(to));
            // указываем тему сообщения
            mailMessage.Subject = subject;
            // Указываем что в теле письма прийдет HTML
            mailMessage.IsBodyHtml = true;
            // указываем текст сообщения
            mailMessage.Body = body;

            // добавляем вложение
            //mailMessage.Attachments.Add(new Attachment(@"C:\\test.txt"));

            return mailMessage;
        }

        private SmtpClient GetSmtpClient()
        {
            // создаём объект SmtpClient
            SmtpClient smtpClient = new SmtpClient(smtp, 587);
            // указываем, что необходимо использовать SSL
            smtpClient.EnableSsl = true;
            // доступ к SMTP серверу
            smtpClient.Credentials = new NetworkCredential(from, pass);

            return smtpClient;
        }

    }
}
