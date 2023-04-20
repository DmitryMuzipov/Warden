using System;
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
            string filename = "Выгрузка.xml";

            // Запрос
            string query = "SELECT tablespace_name, bytes, maxbytes FROM dba_data_files ORDER BY tablespace_name";

            // Создание обьектов
            WorkXML inXml = new WorkXML(filename);


            // Десиериализация JSON
            string path = Path.GetFullPath("Style.css");
            string body = "<!DOCTYPE HTML>";
            body += "<html><head><meta charset =\"utf-8\"><link rel=\"stylesheet\" href=" + path.Replace(@"\" , @"/") + "></head ><body>";
            string message = File.ReadAllText("person.json");
            string config = File.ReadAllText("config.json");
            Massage[] Mes = JsonConvert.DeserializeObject<Massage[]>(message);
            Config[] Conf = JsonConvert.DeserializeObject<Config[]>(config);


            foreach (var con in Conf)
            {
                //ConnectCheck connect = new ConnectCheck(con.Ip);
                //connect.ConnectViev();
                workDB inBD = new workDB(con.ConnectionString);
                inBD.SQLToXml(query, filename);
                inXml.Handler_percent();
                body += "<p>" + con.Server + "</p>";
                body += inXml.XmlToHtml();
            }
            body += "</body></html>";

            Console.WriteLine(path);
            // Отправка письма по списку
            foreach (var per in Mes)
            {
                SendMail send = new SendMail(per.From, per.Subject, body);
                send.Send(per.To);
            }

            Console.Read();
        }
    }
}//bytes, maxbytes
