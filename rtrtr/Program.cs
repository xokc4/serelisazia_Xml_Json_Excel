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
            List<User> jsonUser = JsonFile(FileJson);

            string FileXml = @"C:\Users\stud\Desktop\Новая папка (13)\participants.xml";
           List<User> XmlUser= XmlPopit2(FileXml);

            string FileExcel = @"C:\Users\stud\Desktop\Новая папка (13)\participants.csv";
            List<User> ExcelUser = File222Excel(FileExcel);

            List<UserFull> fulls = UsersFullProgram(jsonUser, XmlUser, ExcelUser);

            Console.WriteLine("напишите слово по нахождении человека");
            string NameUser = Console.ReadLine();

            Search(fulls, NameUser);


            Console.ReadKey();
        }
        public static List<User> JsonFile(string path)
        {
            List<User> сompanies = new List<User>();
            string json = File.ReadAllText(path);// открытие папки
            сompanies = JsonConvert.DeserializeObject<List<User>>(json);//десериализация
            return сompanies;// вывод листа
        }
        public static List<User> XmlPopit2(string path)
        {
            List<User> user = new List<User>();
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
                    user.Add(new User(Name, Surname, date));
                }
            }
            return user;
        }
        public static List<User> File222Excel(string path)
        {
            List<User> users = new List<User>();
            var constew = File.ReadAllText(path, Encoding.UTF8);
            var Conert = constew.Split('\n');
            foreach(var item in Conert)
            {
                var myobject = item.Split(';');
                foreach(var m in myobject)
                {
                    string[] onliStak = m.Split(',');
                    users.Add(new User(onliStak[0], onliStak[1], DateTime.Parse(onliStak[2])));
                }
            }
            return users;
        }
        public static List<UserFull> UsersFullProgram(List<User> jsonUser, List<User> XmlUser, List<User> ExcelUser)
        {
            List<UserFull> users = new List<UserFull>();
            int i = 0;
            foreach(var item in jsonUser)
            {
                i++;
                users.Add(new UserFull(item.FirstName, item.LastName, item.RegistrationDate, i,"Сервис №1"));
            }
            foreach (var item in XmlUser)
            {
                i++;
                users.Add(new UserFull(item.FirstName, item.LastName, item.RegistrationDate, i, "Сервис №2"));
            }
            foreach (var item in XmlUser)
            {
                i++;
                users.Add(new UserFull(item.FirstName, item.LastName, item.RegistrationDate, i, "Сервис №3"));
            }
            return users;
        }
        public static void Search(List<UserFull> fulls, string SearchPeople)
        {
            List<UserFull> usersName = new List<UserFull>(fulls.Where(x=>x.FirstName.Contains(SearchPeople)||x.LastName.Contains(SearchPeople)));
            List<UserFull> users = new List<UserFull>();

            var us = usersName.Select(x => x.ID).Distinct().ToList<int>();
            
            foreach (var item in  us)
            {
                DateTime uzed = usersName.Where(x => x.FirstName.Contains(item) || x.LastName.Contains(item)).Min(x => x.date);


                var u = usersName.Where(x => x.ID == item).Min(x => x.RegistrationDate);
                    users.Add( usersName.Single(x=>x.RegistrationDate == u && x.ID == item));
            }
            users.Sort((x, y) => x.RegistrationDate.CompareTo(y.RegistrationDate));
            foreach(var use in users)
            {
                Console.WriteLine(use.ToString());
            }
        }
    }
}
