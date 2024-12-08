using iLearn.Common;
using iLearn.Data.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iLearn.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private static Guid RoleId1 = Guid.NewGuid();
        private static Guid RoleId2 = Guid.NewGuid();

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public static IList<User> users = new List<User>()
        {
            new User()
            {
                Id = new Guid(),
                FirstName = "sdaa",
                Email = "dashdkj1@ds.com",
                Password = "Password@123",
            },
            new User()
            {
                Id = new Guid(),
                FirstName = "sdaa",
                Email = "dashdkj2@ds.com",
                Password = "Password@123"
            },
            new User()
            {
                Id = new Guid(),
                FirstName = "sdaa",
                Email = "dashdkj3@ds.com",
                Password = "Password@123"
            },
            new User()
            {
                Id = new Guid(),
                FirstName = "sdaa",
                Email = "dashdkj4@ds.com",
                Password = "Password@123"
            },
        };

        public async Task<string> AuthenticateUser(User userToCheck)
        {
            User user = users.FirstOrDefault(u => u.Email == userToCheck.Email && u.Password == userToCheck.Password);

            if (user == null)
            {
                return string.Empty;
            }
            else
            {
                return await generateJwtToken(user);
            }
        }
        public async Task<IList<User>> GetAllUsersAsync()
        {
            return users;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return users.FirstOrDefault(u => u.Id.ToString() == id);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return user;
        }

        /// <summary>
        /// generate and return JWT token for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns> jwt token </returns>
        private async Task<string> generateJwtToken(User user)
        {
            //Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {
                // Get the key from appSettings
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                // create token Descriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]{ new Claim("id", user.Id.ToString()), new Claim("email", user.Email.ToString()) }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
