using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectCheck connect = new ConnectCheck();
            workDB inBD = new workDB();
                     
            connect.ConnectViev("10.225.0.35");
            inBD.SQLquery("select * from ABANDONNED_FOUND LIMIT 100;");
        }
    }
}
//"10.225.20.38"
