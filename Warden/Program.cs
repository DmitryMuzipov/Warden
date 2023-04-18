﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Devart.Data.Oracle;
using Newtonsoft.Json;
using System.IO;

namespace Warden
{
    class Program
    {
        static void Main(string[] args)
        {
            // Сервер
            //string ip = "10.225.0.35";
            //string connectionString = "User id=cds; Password=cds; Server=Prototype;";
            string filename = "Выгрузка.xml";

            // Запрос
            string query = "SELECT tablespace_name, bytes, maxbytes FROM dba_data_files ORDER BY tablespace_name";

            // Создание обьектов
            //ConnectCheck connect = new ConnectCheck(ip);
            //workDB inBD = new workDB(connectionString);
            WorkXML inXml = new WorkXML(filename);
 

            // Обращение к методам
            

            // Десиериализация JSON
            string message = File.ReadAllText("person.json");
            string config = File.ReadAllText("config.json");
            Massage[] Mes = JsonConvert.DeserializeObject<Massage[]>(message);
            Config[] Conf = JsonConvert.DeserializeObject<Config[]>(config);

            var stXML = inXml.StartXML();

            foreach (var con in Conf)
            {
                ConnectCheck connect = new ConnectCheck(con.Ip);
                connect.ConnectViev();
                workDB inBD = new workDB(con.ConnectionString);
                inBD.SQLToXml(query, filename, stXML);
                inXml.Handler_percent();
            }

            inXml.CloseXML(stXML);

            // Сборка тела письма в формат HTML
            string body = inXml.XmlToHtml();
            
            // Отправка письма по списку
            foreach (var per in Mes)
            {
                SendMail send = new SendMail(per.From, per.Subject, body);
                send.Send(per.To);
            }

            //Console.Read();
        }
    }
}//bytes, maxbytes
