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
        public SendMail()
        {

        }
        public void Send()
        {
            // получаем SmtpClient
            SmtpClient smtpClient = GetSmtpClient();
            // получаем сообщение
            MailMessage mailMessage = GetMailMessage();

            // отправляем сообщение
            smtpClient.Send(mailMessage);
        }

        private MailMessage GetMailMessage()
        {
            // создаём объект сообщения
            MailMessage mailMessage = new MailMessage();
            // указываем, от кого отсылается сообщение
            mailMessage.From = new MailAddress("MuzipovDR@tomskneft.ru");
            // указываем, кому отправляется сообщение
            mailMessage.To.Add(new MailAddress("MuzipovDR@tomskneft.ru"));
            // указываем тему сообщения
            mailMessage.Subject = "Тестовый заголовок)";
            // указываем текст сообщения
            mailMessage.Body = "Тестовое сообщение";

            // добавляем вложение
            //mailMessage.Attachments.Add(new Attachment(@"C:\\test.txt"));

            return mailMessage;
        }

        private SmtpClient GetSmtpClient()
        {
            // создаём объект SmtpClient
            SmtpClient smtpClient = new SmtpClient();
            // указываем адрес используемого SMTP сервера
            smtpClient.Host = "mnu-msg-prom18.tomskneft.ru";
            // указываем, что необходимо использовать SSL
            smtpClient.EnableSsl = true;
            // задаём используемый порт
            smtpClient.Port = 587;
            // доступ к SMTP серверу
            smtpClient.Credentials = new NetworkCredential("MuzipovDR@tomskneft.ru", "Ntnhbfylj[2");

            return smtpClient;
        }

    }
}
