using iLearn.Data;
using iLearn.Data.Models;

namespace iLearn.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRoleRepository(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }

        public async Task<bool> AddAsync(UserRole u)
        {
            bool res = false;
            try
            {
                await _dbContext.AddAsync(u);
                _dbContext.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public async Task<ICollection<UserRole>> GetAllAsync()
        {
            ICollection<UserRole> res = null;         
            try
            {
                res = _dbContext.UserRoles.ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return res;
        }
    }
}
