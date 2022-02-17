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
        OracleConnection connection = new OracleConnection("Data Source=Ronyx; User id=onyx; Password=onyx;");
        private void connectDB()
        {
            connection.Open();

            Console.WriteLine("подключение произведено\n");
        }

        private void closeDB()
        {
            connection.Close();

            Console.WriteLine("подключение завершено\n");
        }

        public void SQLquery(string query)
        {
            connectDB();

            OracleCommand command = connection.CreateCommand();
            command.CommandText = query;

            Console.WriteLine("Зпрос к базе данных выполнен\n");
            closeDB();
        }
    }
}
//
//"Data Source=Prototype; User id=cds; Password=cds;"