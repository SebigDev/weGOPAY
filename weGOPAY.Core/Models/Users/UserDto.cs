using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Models.Users
{
    public class UserDto
    {

        public long Id { get; set; }
        public string Fullname { get; set; }

        public string EmailAddress { get; set; }
        public string CountryOfOrigin { get; set; }
        public string CountryOfResidence { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
