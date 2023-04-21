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

            // Создание обьектов
            WorkXML inXml = new WorkXML(filename);

            // Десиериализация JSON
            string body = inXml.HeadHTML();
            string message = File.ReadAllText("person.json");
            string config = File.ReadAllText("config.json");
            Massage[] Mes = JsonConvert.DeserializeObject<Massage[]>(message);
            Config[] Conf = JsonConvert.DeserializeObject<Config[]>(config);

            // Формирование письма
            foreach (var con in Conf)
            {
                workDB inBD = new workDB(con.ConnectionString);
                inBD.SQLToXml(filename);
                inXml.Handler_percent();
                body += "<p>" + con.Server + "</p>";
                body += inXml.XmlToHtml();
            }
            body += inXml.VaultHTML();


            // Отправка письма по списку
            foreach (var per in Mes)
            {
                SendMail send = new SendMail(per.From, per.Subject, body);
                send.Send(per.To);
            }

            //Console.Read();
        }
    }
}
