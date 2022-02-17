using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace rtrtr
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileJson = @"C:\Users\stud\Desktop\Новая папка (13)\participants.json";
            List<UserJson> jsons = JsonFile(FileJson);

            string FileXml = @"C:\Users\stud\Desktop\Новая папка (13)\participants.xml";
           List<UserJson> jsons1= XmlPopit2(FileXml);

            string FileExcel = @"C:\Users\stud\Desktop\Новая папка (13)\participants.csv";

            //List<UserJson> excel = ExcelFile(FileExcel);
            File222Excel();
            Console.ReadKey();
        }
        public static List<UserJson> JsonFile(string path)
        {
            List<UserJson> сompanies = new List<UserJson>();
            string json = File.ReadAllText(path);// открытие папки
            сompanies = JsonConvert.DeserializeObject<List<UserJson>>(json);//десериализация
            return сompanies;// вывод листа
        }
        public static List<UserJson> XmlPopit2(string path)
        {
            List<UserJson> user = new List<UserJson>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    if(xnode == null)
                    {
                        continue;
                    }
                        
                    string Name = string.Empty;
                    string Surname = string.Empty;
                    DateTime date = default;

                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        
                        if (childnode.Name == "Name")
                        {
                            Name = childnode.InnerText; 
                        }
                        if (childnode.Name == "Surname")
                        {
                            Surname = childnode.InnerText;
                        }
                        if (childnode.Name == "RegisterDate")
                        {
                            date =DateTime.Parse(childnode.InnerText);
                        }
                    }
                    user.Add(new UserJson(Name, Surname, date));
                }
            }
            return user;
        }
        public static List<UserJson> ExcelFile(string path)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            List<UserJson> users2 = new List<UserJson>();
            Application ObjExcel = new Application();
            //Открываем книгу.                                                                                                                                                        
            Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            //Выбираем таблицу(лист).
            Worksheet ObjWorkSheet;
            ObjWorkSheet = (Worksheet)ObjWorkBook.Sheets[1];

            // Указываем номер столбца (таблицы Excel) из которого будут считываться данные.
            int numCol = 1;

            Range usedColumn = ObjWorkSheet.UsedRange.Columns[numCol];
            System.Array myvalues = (System.Array)usedColumn.Cells.Value2;
            var strArray = myvalues;
            byte[] xml1;
            foreach(var item in strArray)
            {

                xml1 = UTF8Encoding.UTF8.GetBytes(item.ToString());
                var result = System.Text.Encoding.UTF8.GetString(xml1, 0, xml1.Length);
                Console.WriteLine(result);
            }
            // Выходим из программы Excel.
            ObjExcel.Quit();

            return users2;
        }
        public static void File222Excel()
        {
            string path = @"C:\Users\stud\Desktop\Новая папка (13)\participants.csv";
            var constew = File.ReadAllText(path, Encoding.UTF8);
            var Conert = constew.Split('\n');
            foreach(var item in Conert)
            {
                var myobject = item.Split(';');
                foreach(var m in myobject)
                {
                    Console.WriteLine(m.TrimStart().TrimEnd());
                }
            }
        }
    }
}
