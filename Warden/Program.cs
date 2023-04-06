using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Devart.Data.Oracle;

namespace Warden
{
    class Program
    {
        static void Main(string[] args)
        {
            // Сервер
            string ip = "10.225.0.35";
            string connectionString = "User id=cds; Password=cds; Server=Prototype;";
            string filename = "Выгрузка.xml";

            // Запрос
            string query = "SELECT tablespace_name, bytes, maxbytes FROM dba_data_files ORDER BY tablespace_name";
            // Письмо
            string from = "MuzipovDR@tomskneft.ru";
            string to = "MuzipovDR@tomskneft.ru";
            string subject = "Тестовый заголовок";
            string body = "Тестовое сообщение";

            // Создание обьектов
            ConnectCheck connect = new ConnectCheck(ip);
            workDB inBD = new workDB(connectionString);
            WorkXML inXml = new WorkXML();
            //SendMail send = new SendMail(from, to, subject, body);

            // Обращение к методам
            connect.ConnectViev();
            inBD.SQLquery(query);
            inBD.SQLToXml(query, filename);
            //inXml.Body_writer("Конь.xml");
            //inXml.Reader();
            //send.Send();
        }
    }
}//bytes, maxbytes
