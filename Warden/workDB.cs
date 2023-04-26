using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;
using System.Xml;
//using Devart.Data.Oracle;

namespace Warden
{
    // Класс описывает подключение к базе данных.
    class workDB
    {
        OracleConnection connection = new OracleConnection();
        private string adress;
        private string query = "SELECT +" +
                                    "tablespace_name as tablespaces, " +
                                    "SUM(bytes) as bytes, " +
                                    "SUM(maxbytes) as maxbytes, " +
                                    "SUM(maxbytes - bytes) as remainder, " +
                                    "ROUND((SUM(bytes) / SUM(maxbytes)) * 100, 2) AS Percent " +
                                "FROM dba_data_files WHERE tablespace_name IN " +
                                    "(SELECT tablespace_name " +
                                        "FROM dba_data_files " +
                                        "GROUP BY tablespace_name " +
                                    "HAVING(SUM(bytes) / SUM(maxbytes)) * 100 > 70) " +
                                "GROUP BY tablespace_name " +
                                "ORDER BY tablespace_name ";


        // конструктор
        public workDB(string adress)
        {
            this.adress = adress;
            connection.ConnectionString = adress;
        }

        // подключение к базе данных
        public void connectDB()
        {
            connection.OpenAsync();

            ReadLOG.ReadFile("подключение к базе данных: " + connection.DataSource + "\n" + connection.ServerVersion);
            Console.WriteLine("подключение к базе данных: " + connection.DataSource + "\n" + connection.ServerVersion + "\n");
        }

        // отключение от базы данных
        private void closeDB()
        {
            connection.Close();

            ReadLOG.ReadFile("подключение завершено");
            Console.WriteLine("подключение завершено\n");
        }

        // исполнение запроса к базе данных
        public void SQLquery(string query)
        {
            connectDB();
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

            ReadLOG.ReadFile("Запрос к базе данных выполнен");
            Console.WriteLine("Запрос к базе данных выполнен\n");
            closeDB();
            Console.ReadLine();
        }

        // запись таблици в XML
        public void SQLToXml(string fileName)
        {
            OracleCommand command = connection.CreateCommand();
            

            connectDB();
            command.CommandText = query;

            using (OracleDataReader reader = command.ExecuteReader())
            {
                var KonstGB = 1048575.5f;
                var inGB = 0f;
                var str = "";
                var sum_max_bytes = 0f;
                var count_cell = 0;

                // Создаем объект для записи XML-документа
                XmlTextWriter xmlWriter = new XmlTextWriter(fileName, null);
                xmlWriter.Formatting = Formatting.Indented;

                // Записываем заголовок XML-документа
                xmlWriter.WriteStartDocument();

                // Пишем тело таблицы
                xmlWriter.WriteStartElement("tablespaces");

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) is string)
                        {
                            str = reader.GetValue(i).ToString();
                            xmlWriter.WriteStartElement(str);
                        }
                        else
                        {
                            inGB = Convert.ToUInt64(reader.GetValue(i)) / KonstGB;
                            inGB = (ulong)Math.Floor(inGB);
                            switch (count_cell)
                            {
                                case 0:
                                    xmlWriter.WriteElementString("Value", inGB.ToString());
                                    count_cell++;
                                    break;
                                case 1:
                                    xmlWriter.WriteElementString("MaxValue", inGB.ToString());
                                    count_cell++;
                                    break;
                                case 2:
                                    xmlWriter.WriteElementString("Remainder", inGB.ToString());
                                    count_cell++;
                                    break;
                                default:
                                    xmlWriter.WriteElementString("Percent", reader.GetValue(i).ToString());
                                    count_cell = 0;
                                    break;
                            }
                           
                        }
                    }

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                // Закрываем объект для записи XML-документа
                xmlWriter.Flush();
                xmlWriter.Close();
                //Console.Read();
                ReadLOG.ReadFile("Выгрузка данных завершена");
                //Console.WriteLine("Выгрузка завершена");
            }

            ReadLOG.ReadFile("Запрос к базе данных выполнен. Результат сохранен в файл " + fileName);
            Console.WriteLine("Запрос к базе данных выполнен. Результат сохранен в файл " + fileName);
            closeDB();
            //Console.ReadLine();
        }
    }
}
