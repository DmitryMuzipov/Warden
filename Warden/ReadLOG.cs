using System;
using System.IO;

namespace Warden
{
    public static class ReadLOG
    {
        public static void ReadFile(string str)
        {
            // получаем текущее время и приводим его к строке формата "dd.MM.yyyy HH:mm:ss"
            string time = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            // открываем файл для записи и дописываем в него переданную строку
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine("{0} - {1}", time, str);
            }
        }
    }
}
