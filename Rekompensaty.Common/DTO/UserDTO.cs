using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekompensaty.Common.DTO
{
    public class UserDTO : IDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        

        public string FullName
        {
            get { return Name + " " + Surname; }
        }
    }
}
