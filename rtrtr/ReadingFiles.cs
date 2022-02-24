using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace rtrtr
{
    public class ReadingFiles
    {
        /// <summary>
        /// Чтение Json файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<User> FileJson(string path)
        {
            List<User> сompanies = new List<User>();
            string json = File.ReadAllText(path);// открытие папки
            сompanies = JsonConvert.DeserializeObject<List<User>>(json);//десериализация
            return сompanies;// вывод листа
        }
        /// <summary>
        /// Чтение Xml файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<User> FileXml(string path)
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
                    if (xnode == null)
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
                            date = DateTime.Parse(childnode.InnerText);
                        }
                    }
                    user.Add(new User(Name, Surname, date));
                }
            }
            return user;
        }
        /// <summary>
        /// Чтение Excel файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<User> FileExcel(string path)
        {
            List<User> users = new List<User>();
            var constew = File.ReadAllText(path, Encoding.UTF8);
            var Conert = constew.Split('\n');
            foreach (var item in Conert)
            {
                var myobject = item.Split(';');
                foreach (var m in myobject)
                {
                    string[] onliStak = m.Split(',');
                    users.Add(new User(onliStak[0], onliStak[1], DateTime.Parse(onliStak[2])));
                }
            }
            return users;
        }
        /// <summary>
        /// Сбор всей информации с каждой коллекции в одну 
        /// </summary>
        /// <param name="jsonUser"></param>
        /// <param name="XmlUser"></param>
        /// <param name="ExcelUser"></param>
        /// <returns></returns>
        public static List<UserFull> UsersFull(List<User> jsonUser, List<User> XmlUser, List<User> ExcelUser)
        {
            List<UserFull> users = new List<UserFull>();
            foreach (var item in jsonUser)
            {

                users.Add(new UserFull(item.FirstName, item.LastName, item.RegistrationDate, "Сервис №1"));
            }
            foreach (var item in XmlUser)
            {

                users.Add(new UserFull(item.FirstName, item.LastName, item.RegistrationDate, "Сервис №2"));
            }
            foreach (var item in XmlUser)
            {

                users.Add(new UserFull(item.FirstName, item.LastName, item.RegistrationDate, "Сервис №3"));
            }

            List<UserFull> userFulls = new List<UserFull>();
            foreach (var item in users)
            {
                DateTime uzed = users.Where(x => x.FirstName.Contains(item.FirstName)).Min(x => x.RegistrationDate);
                if (uzed == item.RegistrationDate)
                {

                    userFulls.Add(item);
                }
            }
            userFulls.Sort((x, y) => DateTime.Compare(x.RegistrationDate, y.RegistrationDate));
            List<UserFull> NewLost = new List<UserFull>();
            string FirstName = string.Empty;
            DateTime date = default;
            string LastName = string.Empty;

            foreach (var item in userFulls)
            {
                if (date == item.RegistrationDate && FirstName == item.FirstName && LastName == item.LastName)
                {

                }
                else
                {
                    NewLost.Add(item);
                }
                // don't forget the next row!
                date = item.RegistrationDate;
                FirstName = item.FirstName;
                LastName = item.LastName;
            }
            return NewLost;
        }
        /// <summary>
        /// Объединнение всей логики класса в один метод
        /// </summary>
        /// <param name="PathJson"></param>
        /// <param name="PathXml"></param>
        /// <param name="PathExcel"></param>
        /// <returns></returns>
        public static List<UserFull> UsersMethod(string PathJson, string PathXml, string PathExcel)
        {
            List<User> JsonUser = FileJson(PathJson);

            List<User> XmlUser =  FileXml(PathXml);

            List<User> ExcelUser = FileExcel(PathExcel);

            List<UserFull> UserFull = UsersFull(JsonUser, XmlUser, ExcelUser);

            return UserFull;
        }
    }
}
