using iLearn.Data.Models;

namespace iLearn.Services
{
    public interface IUserService
    {
        Task<string> AuthenticateUser(User userToCheck);
        Task<User> GetUserByIdAsync(string id);

        Task<IList<User>> GetAllUsersAsync();

        Task<User> UpdateUserAsync(User user);
    }
}
