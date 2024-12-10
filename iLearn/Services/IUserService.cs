using iLearn.Data.Models;
using iLearn.Common.Enums;
using iLearn.Data.DTOs;

namespace iLearn.Services
{
    public interface IUserService
    {
        Task<GlobalResponse<string>> AuthenticateUser(User userToCheck);
        Task<GlobalResponse<User>> GetUserByIdAsync(string id);

        Task<GlobalResponse<IList<User>>> GetAllUsersAsync();

        Task<GlobalResponse<User>> UpdateUserAsync(User user);

        Task<GlobalResponse<string>> RegisterUserAsync(User user, Roles userRole);
    }
}
