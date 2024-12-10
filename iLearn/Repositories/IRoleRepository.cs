using iLearn.Data.Models;

namespace iLearn.Repositories
{
    public interface IRoleRepository
    {
        public Task<ICollection<Role>> GetAll();
    }
}
