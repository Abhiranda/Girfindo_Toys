using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GrifindoToysManagementSystem.System_Par
{
    class Setting_compenent
    {
        public bool numbersOnly(string value)
        {
            bool result = false; 
            Regex check = new Regex(@"^[0-9]+$");
            result = check.IsMatch(value);
            return result;
        }
    }
}
