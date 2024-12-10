using iLearn.Data.Models;

namespace iLearn.Repositories
{
    public interface IUserRoleRepository
    {
        Task<ICollection<UserRole>> GetAllAsync();
        Task<bool> AddAsync(UserRole userRole);
    }
}
