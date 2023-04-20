using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden
{
    class Config
    {
        public string Server { get; set; }
        public string Ip { get; set; }
        public string ConnectionString { get; set; }

        public Config(string server, string ip, string connectionString)
        {
            Server = server;
            Ip = ip;
            ConnectionString = connectionString;
        }
    }
}
