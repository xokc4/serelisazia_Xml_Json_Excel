using System;

namespace rtrtr
{
    /// <summary>
    /// класс для работы с юзерами из файлов в программе, наследуюет от (User)
    /// </summary>
    public class UserFull : User
    {
        /// <summary>
        /// конструктор для создания 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="registrationDate"></param>
        /// <param name="supplier"></param>
        public UserFull(string firstName, string lastName, DateTime registrationDate, string supplier) : base(firstName, lastName, registrationDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.RegistrationDate = registrationDate;
            Supplier = supplier;
        }
        public string Supplier { get; set; }

        public override string ToString()
        {
            return $"\t{FirstName}\t\t{LastName}\t{RegistrationDate}\t\t{Supplier}\t";
        }
    }
}