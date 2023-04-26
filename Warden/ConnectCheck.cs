using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden
{
    // Класс проверки подключения к серверу через Ping
    class ConnectCheck
    {
        private string ip;

        // конструктор
        public ConnectCheck(string ip)
        {
            this.ip = ip;
        }

        // проверка подключения
        private Dictionary<string, string> CheckConnect(string ip)
        {
            Ping ping = new Ping();
            PingReply PingReply = ping.Send(ip);

            Dictionary<string, string> connect = new Dictionary<string, string>()
            {
                ["time"] = PingReply.RoundtripTime.ToString(),
                ["status"] = PingReply.Status.ToString(),
                ["address"] = PingReply.Address.ToString(),
            };

            return connect;
        }

        // вывод проверки подключения в консоль
        public void ConnectViev()
        {
            Dictionary<string, string> connect = CheckConnect(ip);

            ReadLOG.ReadFile("время ответа: " + connect["time"]);
            ReadLOG.ReadFile("Статус: " + connect["status"]);
            ReadLOG.ReadFile("ip адресс: " + connect["address"]);
            ReadLOG.ReadFile("Подключение к базе готово");

            Console.WriteLine("время ответа: " + connect["time"]);
            Console.WriteLine("Статус: " + connect["status"]);
            Console.WriteLine("ip адресс: " + connect["address"]);
            Console.WriteLine("готово");
        }
    }
}
