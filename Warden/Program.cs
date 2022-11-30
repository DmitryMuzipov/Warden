using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Devart.Data.Oracle;

namespace Warden
{
    class Program
    {
        static void Main(string[] args)
        {



            ConnectCheck connect = new ConnectCheck("10.225.0.35");
            connect.ConnectViev();

            workDB inBD = new workDB("User id=cds; Password=cds; Server=Prototype;");
            inBD.SQLquery("select * from AA");

            //SendMail send = new SendMail();
            //send.Send();
        }
    }
}
//"10.225.20.38"
