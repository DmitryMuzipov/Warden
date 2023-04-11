﻿using System;
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

            Console.WriteLine("подключение к базе данных: " + connection.DataSource + "\n" + connection.ServerVersion + "\n");
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

            Console.WriteLine("Запрос к базе данных выполнен\n");
            closeDB();
            Console.ReadLine();
        }

        // запись таблици в XML
        public void SQLToXml(string query, string fileName)
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
                var sum_bytes = 0f;
                var count_row = 0;
                var count_cell = 0;
                string stop_tablespace = "AUTH";

                // Создаем объект для записи XML-документа
                XmlTextWriter xmlWriter = new XmlTextWriter(fileName, null);
                xmlWriter.Formatting = Formatting.Indented;

                // Записываем заголовок XML-документа
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("table");
                // Пишем шапку таблицы
                xmlWriter.WriteStartElement("header");
                for (int i = 0; i < reader.FieldCount; i++)
                    xmlWriter.WriteElementString(reader.GetName(i).ToString(), "");
                xmlWriter.WriteEndElement();

                // Пишем тело таблицы
                xmlWriter.WriteStartElement("data");

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) is string)
                        {
                            str = reader.GetValue(i).ToString();
                            xmlWriter.WriteStartElement(str);
                            if (str != stop_tablespace)
                            {
                                sum_max_bytes = 0;
                                sum_bytes = 0;

                            }
                            stop_tablespace = str;
                        }
                        else
                        {
                            inGB = Convert.ToUInt64(reader.GetValue(i)) / KonstGB;
                            inGB = (ulong)Math.Floor(inGB);
                            if (count_cell == 0)
                            {
                                sum_bytes += inGB;
                                xmlWriter.WriteElementString("Value", sum_bytes.ToString());
                                
                                count_cell++;
                            }
                            else
                            {
                                sum_max_bytes += inGB;
                                xmlWriter.WriteElementString("MaxValue", sum_max_bytes.ToString());
                                
                                count_cell = 0;
                            }
                        }
                    }
                    
                    Console.WriteLine(str + " " + inGB + " " + sum_max_bytes);
                    //Console.WriteLine(str);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                // Закрываем объект для записи XML-документа
                xmlWriter.Flush();
                xmlWriter.Close();

                //Console.WriteLine("Выгрузка завершена");
            }

            Console.WriteLine("Запрос к базе данных выполнен. Результат сохранен в файл " + fileName);
            closeDB();
            //Console.ReadLine();
        }
    }
}
