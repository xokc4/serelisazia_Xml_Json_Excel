using System;

namespace rtrtr
{
    public class UserFull : User
    {
        public UserFull(string firstName, string lastName, DateTime registrationDate, int iD, string supplier) : base(firstName, lastName, registrationDate)
        {
            ID = iD;
            Supplier = supplier;
        }
        public int ID { get; set; }
        public string Supplier { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {RegistrationDate} {Supplier}";
        }
    }
}