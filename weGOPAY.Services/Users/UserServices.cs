using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;
using weGOPAY.weGOPAY.Core.Extensions;
using weGOPAY.weGOPAY.Core.Models.Users;
using weGOPAY.weGOPAY.Data;

namespace weGOPAY.weGOPAY.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly weGOPAYDbContext _weGOPAYDbContext;

        public UserServices(weGOPAYDbContext weGOPAYDbContext)
        {
            _weGOPAYDbContext = weGOPAYDbContext;
        }

        public async Task<long> CreateUser(CreateUserDto model)
        {
            //PASSWORD ENCRYPTION
            byte[] passwordHash, passwordSalt;
            CreatePasswordEncrypt(model.Password, out passwordHash, out passwordSalt);
            var nModel = new User
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                EmailAddress = model.EmailAddress,
                Fullname = model.Fullname,
                CountryOfOrigin = model.CountryOfOrigin,
                CountryOfResidence = model.CountryOfResidence,
                Gender = model.Gender.Description(),
                Status = UserStatusEnum.Registered.Description(),
                UserId = Guid.NewGuid().ToString(),
            };
             await _weGOPAYDbContext.User.AddAsync(nModel);
            await _weGOPAYDbContext.SaveChangesAsync();
            return nModel.Id;


        }

        public async Task<User> Login(string email, string password)
        {
            try
            {
                //Get the user
                var logUser = await _weGOPAYDbContext.User
                             .FirstOrDefaultAsync(x => x.EmailAddress == email);
                if (logUser == null)
                {
                    return null;
                }

                //check password
                if (!VerifyPasswordHash(password, logUser.PasswordHash, logUser.PasswordSalt))
                {
                    return null;
                }
                //Authentication is succefull
                return logUser;
            }
            catch (Exception)
            {
                return null;
            }

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }
    

        public async Task DeleteUser(long id)
        {
            try
            {
                var dUser = await _weGOPAYDbContext.User.FindAsync(id);
                if (dUser == null) return;
                _weGOPAYDbContext.User.Remove(dUser);
                await _weGOPAYDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            try
            {
                var userDto = new List<UserDto>();

                var allUsers = await _weGOPAYDbContext.User.ToListAsync();
                userDto.AddRange(allUsers.OrderByDescending(s => s.DateRegistered).Select(x => new UserDto()
                {
                    Id = x.Id,
                    Fullname = x.Fullname,
                    EmailAddress = x.EmailAddress,
                    CountryOfOrigin = x.CountryOfOrigin,
                    CountryOfResidence = x.CountryOfResidence,
                    Gender = x.Gender,
                    Status = x.Status,
                    DateRegistered = x.DateRegistered
                }).ToList());
                var allUserList = userDto;
                return allUserList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDto> GetUser(long id)
        {
            try
            {
                var x = await _weGOPAYDbContext.User.Where(s => s.Id == id).FirstOrDefaultAsync();
                if (x == null) return null;
                var nUser = new UserDto
                {
                    Id = x.Id,
                    Fullname = x.Fullname,
                    EmailAddress = x.EmailAddress,
                    CountryOfOrigin = x.CountryOfOrigin,
                    CountryOfResidence = x.CountryOfResidence,
                    Gender = x.Gender,
                    Status = x.Status,
                    DateRegistered = x.DateRegistered
                };
                return nUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<UserDto> GetUserByUserId(string userId)
        {
            try
            {
                var x = await _weGOPAYDbContext.User.Where(s => s.UserId == userId).FirstOrDefaultAsync();
                if (x == null) return null;
                var nUser = new UserDto
                {
                    Id = x.Id,
                    Fullname = x.Fullname,
                    EmailAddress = x.EmailAddress,
                    CountryOfOrigin = x.CountryOfOrigin,
                    CountryOfResidence = x.CountryOfResidence,
                    Gender = x.Gender,
                    Status = x.Status,
                    DateRegistered = x.DateRegistered
                };
                return nUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public async Task UpdateUser(UpdateUserDto model, long id)
        {
            try
            {
                var uUser = await _weGOPAYDbContext.User.FindAsync(id);
                if (uUser == null) return;
                uUser.Id = model.Id;
                uUser.Fullname = model.Fullname;
                uUser.CountryOfResidence = model.CountryOfResidence;
                uUser.DateUpdated = DateTime.UtcNow;
                uUser.IsUpdated = true;

                _weGOPAYDbContext.Entry(uUser).State = EntityState.Modified;
                await _weGOPAYDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region Helper

        public string GenerateToken(User user)
        {
            //Generating Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Super Secret Key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                   new Claim(ClaimTypes.Name, user.EmailAddress)
               }),
                Expires = DateTime.Now.AddDays(2.0),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }



        //CREATION OF PASSWORD ENCRYPTION
        private void CreatePasswordEncrypt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> GetUserByEmailAddress(string email)
        {
            var check = await _weGOPAYDbContext.User.Where(e => e.EmailAddress == email).FirstOrDefaultAsync();
            if (check != null) return true;
            return false;
        }


        //CHECKING IF USER EXIST ALREADY
        public async Task<bool> UserExists(string emailAddress)
        {
            if (await _weGOPAYDbContext.User
                        .AnyAsync(x =>x.EmailAddress == emailAddress))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserServices() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

     
        #endregion

    }
}
