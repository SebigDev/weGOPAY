using weGOPAY.weGOPAY.Core.Enums;

namespace weGOPAY.weGOPAY.Core.Models.Users
{
    public class CreateUserDto
    {

        public string Fullname { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }        public string CountryOfOrigin { get; set; }
        public string CountryOfResidence { get; set; }
        public GenderEnum Gender { get; set; }
        public UserStatusEnum Status { get; set; }

    }
}
