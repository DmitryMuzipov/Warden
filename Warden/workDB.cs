using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace Warden
{
    // Класс описывает подключение к базе данных.
    class workDB
    {
        OracleConnection connection = new OracleConnection();
        private static string adress;

        // конструктор
        public workDB(string address)
        {
            adress = address;
            connection.ConnectionString = address;
        }

        // подключение к базе данных
        private void connectDB()
        {
            connection.Open();

            Console.WriteLine("подключение произведено\n");
        }

        // отключение от базы данных
        private void closeDB()
        {
            connection.Close();

            Console.WriteLine("подключение завершено\n");
        }

        // исполнение запроса к базе данных
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