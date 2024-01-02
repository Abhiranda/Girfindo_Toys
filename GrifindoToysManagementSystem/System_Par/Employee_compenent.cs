using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GrifindoToysManagementSystem.System_Par
{
    class Employee_compenent
    {
        public bool textOnly(string value) 
        {
            bool result = false;
            Regex check = new Regex(@"^[a-zA-Z\s]+$");
            result = check.IsMatch(value);
            return result;
        }
    }
}
