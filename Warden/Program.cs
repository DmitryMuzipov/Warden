using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Devart.Data.Oracle;

namespace Warden
{
    class Program
    {
        static void Main(string[] args)
        {
            // Сервер
            string ip = "10.225.0.35";
            string connectionString = "User id=cds; Password=cds; Server=Prototype;";
            // Запрос
            string query = "select * from AA";
            // Письмо
            string from = "MuzipovDR@tomskneft.ru";
            string to = "MuzipovDR@tomskneft.ru";
            string subject = "Тестовый заголовок";
            string body = "Тестовое сообщение";


            ConnectCheck connect = new ConnectCheck(ip);
            connect.ConnectViev();

            workDB inBD = new workDB(connectionString);
            inBD.SQLquery(query);

            SendMail send = new SendMail(from, to, subject, body);
            send.Send();
        }
    }
}
//"10.225.20.38"
