using ConsoleTables;
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
        private static string FileJson = @"participants.json";//путь для файла Json
        private static string FileXml = @"participants.xml";//путь для файла Xml
        private static string FileExcel = @"participants.csv";//путь для файла Excel
        static void Main(string[] args)
        {
            LogicSection();
            
        }
        /// <summary>
        /// Разделение логики программы на две команды 
        /// </summary>
        public static void LogicSection()
        {
            List<UserFull> fulls = ReadingFiles.UsersMethod(FileJson, FileXml, FileExcel);
            
            char InfiniteLoop = 'g';
            do
            {
                Console.WriteLine("Введите команду");
                string Message = Console.ReadLine();

                string[] MessageBox = Message.Split(' ');
                switch (MessageBox[0])
                {
                    case "get-page":
                        try
                        {
                            List<UserFull> users = WithdrawalEmployees.SearchList(fulls, MessageBox[1]);
                            TablePeople(users);
                        }
                        catch
                        {
                            Console.WriteLine("написали не правильную команду");
                        }
                        break;
                    case "search":
                        try
                        {
                            List<UserFull> UsersList = WithdrawalEmployees.Search(fulls, MessageBox[1]);
                            TablePeople(UsersList);
                        }
                        catch
                        {
                            Console.WriteLine("написали не правильную команду");
                        }
                        break;
                    default:
                        Console.WriteLine("нет такой команды");
                        break;
                }
            } while(InfiniteLoop == 'g');
            
            Console.ReadKey();
        }
        /// <summary>
        /// Вывод данных с помощью таблицы 
        /// </summary>
        /// <param name="fulls"></param>
        public static void TablePeople(List<UserFull> fulls)
        {
            var table = new ConsoleTable("Имя", "Фамилия", "Дата регистрации", "Поставщик");
            foreach (var use in fulls)
            {
                table.AddRow(use.FirstName, use.LastName, use.RegistrationDate, use.Supplier);
            }
            table.Write();
        }
    }
}
