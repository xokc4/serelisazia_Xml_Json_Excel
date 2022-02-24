using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtrtr
{
    /// <summary>
    /// класс для чтение файлов 
    /// </summary>
   public class User 
    {
        public User()
        {
        }
        /// <summary>
        /// Конструктор человека для файла
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="registrationDate"></param>
        public User(string firstName, string lastName, DateTime registrationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            RegistrationDate = registrationDate;
        }

        public string FirstName { get; set; }//Имя
        public string LastName { get; set; }//Фамилия

        public DateTime RegistrationDate { get; set; }//дата

        public override string ToString()//вывод данных
        {
            return $"{FirstName}    {LastName}     {RegistrationDate}";
        }
    }
}
