using iLearn.Common;
using iLearn.Common.Enums;
using iLearn.Data.DTOs;
using iLearn.Data.Models;
using iLearn.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iLearn.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IOptions<AppSettings> appSettings, 
            IUserRepository userRepository, 
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public static IList<User> users = new List<User>();

        public async Task<GlobalResponse<string>> AuthenticateUser(User userToCheck)
        {
            GlobalResponse<string> response = new GlobalResponse<string>();
            try
            {
                User user = await _userRepository.GetByEmailAsync(userToCheck.Email);

                if (user?.Password == userToCheck.Password)
                {
                    var jwt = await generateJwtToken(user);
                    response = new GlobalResponse<string>()
                    {
                        Data = jwt,
                    };
                }
                else
                {
                    response = new GlobalResponse<string>()
                    {
                        ErrorCode = ErrorCodes.UserNotFound.ErrorCode,
                        Message = ErrorCodes.UserNotFound.ErrorMessage,
                        Data = null,
                    };
                }
            }
            catch(Exception ex)
            {
                response = new GlobalResponse<string>()
                {
                    ErrorCode = ErrorCodes.SomethingWentWrong.ErrorCode,
                    Message = ErrorCodes.SomethingWentWrong.ErrorMessage,
                    Data = null,
                };
            }
            return response;
        }
        public async Task<GlobalResponse<IList<User>>> GetAllUsersAsync()
        {
            return new GlobalResponse<IList<User>>();
        }

        public async Task<GlobalResponse<User>> GetUserByIdAsync(string id)
        {
            GlobalResponse<User> res = new GlobalResponse<User>();
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    res = new GlobalResponse<User>()
                    {
                        ErrorCode = ErrorCodes.UserNotFound.ErrorCode,
                        Message = ErrorCodes.UserNotFound.ErrorMessage,
                    };
                }
            }
            catch(Exception ex)
            {
                res = new GlobalResponse<User>()
                {
                    ErrorCode = ErrorCodes.SomethingWentWrong.ErrorCode,
                    Message = ErrorCodes.SomethingWentWrong.ErrorMessage,
                };
            }
            return new GlobalResponse<User>();
            //return users.FirstOrDefault(u => u.Id.ToString() == id);
        }

        public async Task<GlobalResponse<User>> UpdateUserAsync(User user)
        {
            return new GlobalResponse<User>();
            //return user;
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
        public async Task<GlobalResponse<string>> RegisterUserAsync(User user, Roles userRole)
        {
            GlobalResponse<string> res = new GlobalResponse<string>();
            try
            {
                // check if user already exists
                var existingUser = await _userRepository.GetByEmailAsync(user.Email);
                
                if (existingUser == null)
                {
                    // user does not exist, add user to db
                    var userAdded = await _userRepository.AddUserAsync(user);
                    if (userAdded)
                    {
                        // Add Role to the user
                        if (await this.AddUserRole(user.Id, userRole))
                        {
                            var jwt = await this.generateJwtToken(user);
                            res = new GlobalResponse<string>()
                            {
                                Data = jwt
                            };
                        }
                        else
                        {
                            res = new GlobalResponse<string>()
                            {
                                ErrorCode = ErrorCodes.DbError.ErrorCode,
                                Message = $"{ErrorCodes.DbError.ErrorMessage} while adding user-role",
                            };
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    //user already exists, check if user already have the requested role
                    var isExistingRole = true;
                    switch (userRole)
                    {
                        case Roles.User:
                            if (existingUser.UserRoles.FirstOrDefault(u => u.Role.RoleName == "User") == null)
                            {
                                isExistingRole = false;
                            }
                            break;
                        case Roles.Instructor:
                            if (existingUser.UserRoles.FirstOrDefault(u => u.Role.RoleName == "Instructor") == null)
                            {
                                isExistingRole = false;
                            }
                            break;
                    }

                    if (!isExistingRole)
                    {
                        // Add Role to the user
                        if(await this.AddUserRole(existingUser.Id, userRole))
                        {
                            var jwt = this.generateJwtToken(user);
                            res = new GlobalResponse<string>()
                            {
                                Data = await jwt
                            };
                        }
                        else
                        {
                            res = new GlobalResponse<string>()
                            {
                                ErrorCode = ErrorCodes.DbError.ErrorCode,
                                Message = $"{ErrorCodes.DbError.ErrorMessage} while adding user-role",
                            };
                        }
                    }
                    else
                    {
                        var err = ErrorCodes.UserAlreadyExist;
                        res = new GlobalResponse<string>()
                        {
                            ErrorCode = ErrorCodes.UserAlreadyExist.ErrorCode,
                            Message = ErrorCodes.UserAlreadyExist.ErrorMessage
                        };
                    }
                    
                }

            }
            catch (Exception ex)
            {
                res = new GlobalResponse<string>()
                {
                    ErrorCode = ErrorCodes.SomethingWentWrong.ErrorCode,
                    Message = ex.Message.ToString()
                };
            }
            return res;
        }

        private async Task<bool> AddUserRole(Guid userId, Roles userRole)
        {
            var res = false;
            var roleId = string.Empty;
            try
            {
                // get all the roles
                var roles = await _roleRepository.GetAll();

                switch (userRole)
                {
                    case Roles.User:
                        roleId = roles.FirstOrDefault(u => u.RoleName == "User")?.Id.ToString();
                        break;
                    case Roles.Instructor:
                        roleId = roles.FirstOrDefault(u => u.RoleName == "Instructor")?.Id.ToString();
                        break;
                }

                if (!string.IsNullOrEmpty(roleId))
                {
                    // save userRoles to DB
                    await _userRoleRepository.AddAsync(new UserRole()
                    {
                        UserId = userId,
                        RoleId = new Guid(roleId)
                    });
                    res = true;
                }
            }
            catch
            {

            }
            return res;
        }
    }
}
