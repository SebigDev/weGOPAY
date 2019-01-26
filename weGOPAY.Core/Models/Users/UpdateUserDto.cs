using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Models.Users
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
      
        public string CountryOfResidence { get; set; }
      
        public DateTime DateUpdated { get; set; }
    }
}
