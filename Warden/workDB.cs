using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.OracleClient;
using Devart.Data.Oracle;

namespace Warden
{
    // Класс описывает подключение к базе данных.
    class workDB
    {
        //OracleConnection connection = new OracleConnection();
        //private static string adress;

        // конструктор
        //public workDB(string address)
        //{
        //    adress = address;
        //    connection.ConnectionString = address;
        //}

        // подключение к базе данных
        //public void connectDB()
        //{
        //    connection.OpenAsync();

        //    Console.WriteLine("подключение к базе данных: " + connection.DataSource + "\n" + connection.ServerVersion + "\n");
        //}

        // отключение от базы данных
        //private void closeDB()
        //{
        //    connection.Close();

        //    Console.WriteLine("подключение завершено\n");
        //}

        // исполнение запроса к базе данных
        public void SQLquery(string query, OracleConnection connection)
        {
            OracleCommand command = connection.CreateCommand();
            command.CommandText = query;

            using (OracleDataReader reader = command.ExecuteReader())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write(reader.GetName(i).ToString() + "\t");
                Console.Write(Environment.NewLine);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.Write(reader.GetValue(i).ToString() + "\t");
                    Console.Write(Environment.NewLine);
                }
            }

            //command.CommandText = query;

            //using (OracleCommand cmd = connection.CreateCommand())
            //{
            //cmd.CommandText = query;


            //using (OracleDataReader reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        Console.WriteLine(reader.GetString(0) + " " + reader.GetString(1));
            //    }
            //}
            //}

            Console.WriteLine("Запрос к базе данных выполнен\n");
            //closeDB();
        }
    }
}
