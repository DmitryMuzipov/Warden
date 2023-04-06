using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Warden
{
    class WorkXML
    {
        public WorkXML()
        {
            
        }

        public void Reader()
        {
            XmlReader xmlFile = XmlReader.Create("Выгрузка.xml");
            while (xmlFile.Read())
            {
                if (xmlFile.NodeType == XmlNodeType.Element)
                {
                    if (xmlFile.Name == "Name")
                    {
                        Console.WriteLine("Name: " + xmlFile.ReadInnerXml());
                    }
                    else if (xmlFile.Name == "Value")
                    {
                        Console.WriteLine("Value: " + xmlFile.ReadInnerXml());
                    }
                }
            }

            Console.ReadLine();
        }

        public void Handler_repet()
        {
            // Загружаем xml файл в XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("выгрузка.xml");

            // Получаем корневой элемент документа и коллекцию всех дочерних элементов
            XmlNode root = xmlDoc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes("data/*");

            // Проходимся по всем дочерним элементам
            for (int i = nodeList.Count - 2; i >= 0; i--)
            {
                // Получаем текущий и следующий элементы
                XmlNode currentNode = nodeList.Item(i);
                XmlNode nextNode = nodeList.Item(i + 1);

                // Если названия элементов совпадают, то удаляем текущий элемент
                if (currentNode.Name == nextNode.Name)
                {
                    root.SelectSingleNode("data").RemoveChild(currentNode);
                }
            }

            // Сохраняем изменения в файл
            xmlDoc.Save("выгрузка-new.xml");
        }

        // Важно Header_writer и Vault_writer работают в паре если открывается элемент в шапке то должен быть закрыт в подвале
        private void Header_writer(string fileName, XmlTextWriter xmlWriter)
        {
            // Параметр отвечающий за лесечный формат элементов в файле
            xmlWriter.Formatting = Formatting.Indented;

            // Записываем заголовок XML-документа
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("table");
            // Пишем шапку таблицы
            xmlWriter.WriteStartElement("header");
            xmlWriter.WriteElementString("Место", "Тут");
            xmlWriter.WriteEndElement();




            //Console.WriteLine("Выгрузка завершена");
        }

        private void Vault_writer(string fileName, XmlTextWriter xmlWriter)
        {
            //XmlTextWriter xmlWriter = new XmlTextWriter(fileName, null);

            // Закрываем объект для записи XML-документа
            
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

            Console.WriteLine("Запись произведена");
            //Console.ReadLine();
        }

        public void Body_writer(string fileName)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(fileName, null);

            // Шапка Файла
            Header_writer(fileName, xmlWriter);

            // Пишем тело таблицы
            xmlWriter.WriteStartElement("Ферма");

            xmlWriter.WriteStartElement("Стойло");
            xmlWriter.WriteElementString("Место", "Тут конь валялся!");
            xmlWriter.WriteEndElement();




            // Подвал файлаxmlWriter.WriteEndElement();
            Vault_writer(fileName, xmlWriter);
        }
    }
}
