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

        // Формирование HTML Шапки и стилей
        public string HeadHTML()
        {
            string html = "<!DOCTYPE HTML>"+
                              "<html><head><meta charset =\"utf-8\"><style>" +
                                "table{border: 4px solid black;}" +
                                "td{border: 1px solid black;" +
                                   "padding: 5px; margin: 5px;}" +
                                "th{margin-left: 15px;}" +
                                "td.reed{background: #e05858;}" +
                                "tr {border: 2px solid black;}" +
                                "p{font-size: 15px;}" +
                              "</style></head ><body>";

            return html;
        }

        public string VaultHTML()
        {
            string html = "</ body ></ html >";

            return html;
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
