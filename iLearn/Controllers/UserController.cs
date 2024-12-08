using iLearn.Common;
using iLearn.Data.Models;
using iLearn.Services;
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

        [HttpGet("login")]        
        public async Task<string> Login()
        {
            var token = await _userService.AuthenticateUser(new User
            {
                Email = "dashdkj1@ds.com",
                Password = "Password@123"
            });
            return token;
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
