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

            // Создание обьектов
            ConnectCheck connect = new ConnectCheck(ip);
            workDB inBD = new workDB(connectionString);
            SendMail send = new SendMail(from, to, subject, body);

            // Обращение к функциям
            connect.ConnectViev();
            inBD.SQLquery(query);
            send.Send();
        }
    }
}
