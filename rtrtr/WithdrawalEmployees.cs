using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtrtr
{
    public class WithdrawalEmployees
    {
        /// <summary>
        /// метод для поиска людей по части имени
        /// </summary>
        /// <param name="fulls"></param>
        /// <param name="SearchPeople"></param>
        /// <returns></returns>
        public static List<UserFull> Search(List<UserFull> fulls, string SearchPeople)
        {
            string[] PartName = SearchPeople.Split('"');
            string part = PartName[1];
            List<UserFull> usersName = new List<UserFull>(fulls.Where(x => x.FirstName.Contains(part) || x.LastName.Contains(part)));
            List<UserFull> users = new List<UserFull>();

            foreach (var item in usersName)
            {
                DateTime uzed = usersName.Where(x => x.FirstName.Contains(item.FirstName)).Min(x => x.RegistrationDate);
                if (uzed == item.RegistrationDate)
                {

                    users.Add(item);
                }
            }
            users.Sort((x, y) => x.RegistrationDate.CompareTo(y.RegistrationDate));

            return users;
        }
        /// <summary>
        /// вывод людей с листа 
        /// </summary>
        /// <param name="fulls"></param>
        /// <param name="SreachPeople"></param>
        /// <returns></returns>
        public static List<UserFull> SearchList(List<UserFull> fulls, string SreachPeople)
        {
            List<UserFull> users = new List<UserFull>();
                int colPan = (Convert.ToInt32(SreachPeople) - 1) * 5;
                int s4et = 0;

                foreach (var item in fulls)
                {
                    if (fulls.IndexOf(item) == colPan)
                    {
                        if (s4et < 5)
                        {
                            users.Add(item);
                            colPan++;
                            s4et++;
                        }
                    }
                }
            return users;
        }
    }
}
