using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden
{
    class Config
    {
        public string Ip { get; set; }
        public string ConnectionString { get; set; }

        public Config(string ip, string connectionString)
        {
            Ip = ip;
            ConnectionString = connectionString;
        }
    }
}
