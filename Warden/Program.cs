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
            ConnectCheck connect = new ConnectCheck("10.225.0.35");
            workDB inBD = new workDB("Data Source=Ronyx; User id=onyx; Password=onyx;");
                     
            connect.ConnectViev();
            inBD.SQLquery("select * from ABANDONNED_FOUND LIMIT 100;");
        }
    }
}
//"10.225.20.38"
