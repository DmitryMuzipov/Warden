using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devart.Data.Oracle;

namespace Warden
{
    class Program
    {
        static void Main(string[] args)
        {



            //ConnectCheck connect = new ConnectCheck("10.225.0.35");
            //connect.ConnectViev();

            //using (OracleConnection conn = new OracleConnection("User id=cds; Password=cds; Server=Prototype;"))
            //{
            //    try
            //    {
            //        conn.OpenAsync();
            //        Console.WriteLine("подключение к базе данных: " + conn.DataSource + "\n" + conn.ServerVersion + "\n");

            //        workDB program = new workDB();
            //        program.SQLquery("select * from AA", conn);

            //    }
            //    catch (OracleException ex)
            //    {
            //        Console.WriteLine("Exception occurs: {0}", ex.Message);
            //    }
            //    finally
            //    {
            //        Console.ReadLine();
            //    }
            //}


            //workDB inBD = new workDB("User id=cds; Password=cds; Server=Prototype;");
            //inBD.SQLquery("select * from AA");

            SendMail send = new SendMail();
            send.Send();
        }
    }
}
//"10.225.20.38"
