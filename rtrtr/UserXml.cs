using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtrtr
{
    public class UserXml
    {
        public UserXml()
        {
        }

        public UserXml(string name, string surname, DateTime registerDate)
        {
            Name = name;
            Surname = surname;
            RegisterDate = registerDate;
        }

       public string Name { get; set; }
       public string Surname { get; set; }
       public DateTime RegisterDate { get; set;}

    }
}
