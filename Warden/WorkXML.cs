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
        string fileName;

        public WorkXML(string fileName)
        {
            this.fileName = fileName;
        }

        private void Handler_repet()
        {
            // Загружаем xml файл в XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

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

        public void Handler_percent()
        {
            // Избавляемся от дубликатов записей
            Handler_repet();

            // Создаем XML документ
            XmlDocument doc = new XmlDocument();
            doc.Load("выгрузка-new.xml"); // Путь к исходному файлу
            XmlElement root = doc.DocumentElement;

            // Создаем новый XML документ с корневым элементом tablespaces
            XmlDocument newDoc = new XmlDocument();
            var newRoot = newDoc.CreateElement("tablespaces");
            newDoc.AppendChild(newRoot);

            // Получаем список элементов данных таблиц
            XmlNodeList tables = root.SelectNodes("data/*");

            // Проходимся по каждому элементу
            foreach (XmlNode table in tables)
            {
                int value = int.Parse(table.SelectSingleNode("Value").InnerText);
                int maxValue = int.Parse(table.SelectSingleNode("MaxValue").InnerText);

                // Вычисляем процент заполненности таблицы
                double percent = (double)value / maxValue;
                double remainder = maxValue - value;

                // Если процент заполненности больше 0.7, добавляем элемент в новый файл
                if (percent > 0.7)
                {
                    // Сокращаем проценты
                    percent = percent * 100;

                    // Создаем новый элемент для таблицы
                    XmlElement newTable = newDoc.CreateElement(table.Name);

                    // Добавляем элементы Value, MaxValue и Percent
                    newTable.AppendChild(newDoc.CreateElement("Value")).InnerText = value.ToString() + " GB";
                    newTable.AppendChild(newDoc.CreateElement("MaxValue")).InnerText = maxValue.ToString() + " GB";
                    newTable.AppendChild(newDoc.CreateElement("Remainder")).InnerText = remainder.ToString("0") + " MB";
                    newTable.AppendChild(newDoc.CreateElement("Percent")).InnerText = percent.ToString("0") + "%";

                    // Добавляем новый элемент таблицы в корневой элемент нового файла
                    newRoot.AppendChild(newTable);
                }
            }

            // Сохраняем новый XML файл
            newDoc.Save("выгрузка.xml");
        }

        public string XmlToHtml()
        {
            // Создаем обьект XML и загружаем в него файл
            XmlDocument doc = new XmlDocument();
            doc.Load("выгрузка.xml");
            string cl = "reed";

            // Пишем строку с шапкой и заголовки колонок
            string html = "<table>";
            html += "<tr><th>Табличное пространство</th><th>Масимальное значения</th><th>Текущее заначение</th><th>Остаток места</th><th>Проценты</th></tr>";

            // Заполняем html таблицу банными их xml файла
            XmlNodeList tablespaceNodes = doc.GetElementsByTagName("tablespaces")[0].ChildNodes;
            foreach (XmlNode node in tablespaceNodes)
            {
                string tablespace = node.Name;
                string value = node["Value"].InnerText;
                string maxValue = node["MaxValue"].InnerText;
                string remainder = node["Remainder"].InnerText;
                string percent = node["Percent"].InnerText;

                html += "<tr>";
                html += "<td>" + tablespace + "</td>";
                html += "<td>" + maxValue + "</td>";
                html += "<td>" + value + "</td>";
                html += "<td>" + remainder + "</td>";
                html += "<td class=" + cl + ">" + percent + "</td>";
                html += "</tr>";
            }

            html += "</table>";

            //Console.WriteLine(html);
            return html;
        }
    }
}
