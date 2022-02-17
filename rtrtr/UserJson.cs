using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtrtr
{
   public class UserJson
   {
        public UserJson()
        {
        }
        public UserJson(string firstName, string lastName, DateTime registrationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            RegistrationDate = registrationDate;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }

    }
}
