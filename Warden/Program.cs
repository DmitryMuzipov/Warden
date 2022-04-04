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
            connect.ConnectViev();

            workDB inBD = new workDB("Data Source=Prototype; User id=cds; Password=cds;");
            inBD.SQLquery("");

            //SendMail send = new SendMail();
            //send.Send();
        }
    }
}
//"10.225.20.38"
