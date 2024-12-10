using iLearn.Data.Models;

namespace iLearn.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);

        Task<User> GetUserByIdAsync(string userId);

        Task<User> GetByEmailAsync(string email);

        //Task<User> GetByEmailAndPasswordAsync(string email, string password);
    }
}
