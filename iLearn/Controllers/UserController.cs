using iLearn.Common;
using iLearn.Data.DTOs;
using iLearn.Data.Models;
using iLearn.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace iLearn.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<GlobalResponse<string>> Register(UserRegistrationDTO register)
        {
            var response = new GlobalResponse<string>();
            try
            {
                User user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Password = register.Password
                };

                response = await _userService.RegisterUserAsync(user, register.Role);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        [HttpGet("login")]        
        public async Task<IActionResult> Login(UserLoginDTO login)
        {
            var res = new GlobalResponse<string>();
            try
            {
                var isReqValid = UserLoginDTO.ValidateRequest(login);
                if(!isReqValid.HasError)
                {
                    User user = new User()
                    {
                        Email = login.Email,
                        Password = login.Password,
                    };
                    res = await _userService.AuthenticateUser(user);
                }
                else
                {
                    return BadRequest(isReqValid);
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(res);
        }

        [HttpGet("user")]
        [Authorize("Admin,Instructor")]
        public async Task<string> GetUser()
        {
            User user = (User)HttpContext.Items["User"];
            return "";
        }
    }
}
