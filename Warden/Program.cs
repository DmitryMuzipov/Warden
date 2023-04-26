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
            FormationHTML inHTML = new FormationHTML(filename);

            // Десиериализация JSON
            string message = File.ReadAllText("person.json");
            string config = File.ReadAllText("config.json");
            Massage[] Mes = JsonConvert.DeserializeObject<Massage[]>(message);
            Config[] Conf = JsonConvert.DeserializeObject<Config[]>(config);

            // Формирование письма
            string body = inHTML.HeadHTML();
            foreach (var con in Conf)
            {
                ConnectCheck сcon = new ConnectCheck(con.Ip);
                сcon.ConnectViev();
                workDB inBD = new workDB(con.ConnectionString);
                inBD.SQLToXml(filename);
                body += "<p>" + con.Server + "</p>";
                body += inHTML.XmlToHtml();
            }
            body += inHTML.VaultHTML();


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
