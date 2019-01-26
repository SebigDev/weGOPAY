using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Models.Users;

namespace weGOPAY.weGOPAY.Services.Users
{
    public interface IUserServices : IDisposable
    {
        Task<long> CreateUser(CreateUserDto model);
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string emailAddress);
        Task UpdateUser(UpdateUserDto model, long id);
        Task DeleteUser(long id);

        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(long id);
        Task<bool> GetUserByEmailAddress(string email);

        Task<UserDto> GetUserByUserId(string userId);

        string GenerateToken(User user);


    }
}
