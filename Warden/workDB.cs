using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace Warden
{
    class workDB
    {
        string addres = "Data Source=Ronyx; User id=onyx; Password=onyx;";
        private void connectDB()
        {
            OracleConnection connection = new OracleConnection(addres);
            connection.Open();

            Console.WriteLine("подключение произведено\n");
        }

        private void closeDB()
        {
            OracleConnection connection = new OracleConnection(addres);
            connection.Close();

            Console.WriteLine("подключение завершено\n");
        }

        public void SQLquery(string query)
        {
            connectDB();

            OracleConnection connection = new OracleConnection(addres);
            OracleCommand command = connection.CreateCommand();
            command.CommandText = query;

            Console.WriteLine("Зпрос к базе данных выполнен\n");
            closeDB();
        }
    }
}
//
//"Data Source=Prototype; User id=cds; Password=cds;"