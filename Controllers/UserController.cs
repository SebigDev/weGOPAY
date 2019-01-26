using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weGOPAY.weGOPAY.Core.Models.Users;
using weGOPAY.weGOPAY.Services.Users;

namespace weGOPAY.Controllers
{

    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserServices _userService;


        public UserController(IUserServices userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(UserDto))]
        [ProducesResponseType(typeof(IEnumerable<UserDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var allUsers = await _userService.GetAllUsers();
                return Ok(allUsers);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(UserDto))]
        [ProducesResponseType(typeof(UserDto),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUser([FromQuery] long id)
        {
            try
            {
                var user = await _userService.GetUser(id);
                if (user == null) return NotFound();
                return Ok(user);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(long))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUser)
        {
            try
            {
                var check = await _userService.GetUserByEmailAddress(createUser.EmailAddress);
                if (check == true) return BadRequest($"{createUser.EmailAddress} already exists on our database, please choose another email address");
                var nUser = await _userService.CreateUser(createUser);
                return Ok(nUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(UserDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(string emailAddress, string password)
        {
            try
            {
                var nCheck = await _userService.UserExists(emailAddress);
                if (nCheck == false) return NotFound($"User with Email: {emailAddress} does not exist on our database");
                var logUser = await _userService.Login(emailAddress, password);
                if(logUser == null)
                {
                  return Unauthorized();
                }
                
                var token = _userService.GenerateToken(logUser);
               
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

     

        [HttpPut]
        [Route("[action]")]
        [Produces(typeof(UserDto))]
        [ProducesResponseType(typeof(UserDto),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUser, long id)
        {
            try
            {
                await _userService.UpdateUser(updateUser, id);
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("[action]")]
        [Produces(typeof(UserDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
