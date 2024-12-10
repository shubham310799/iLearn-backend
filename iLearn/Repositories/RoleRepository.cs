using iLearn.Data;
using iLearn.Data.Models;

namespace iLearn.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _appDbContext;

        public RoleRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<ICollection<Role>> GetAll()
        {
            ICollection<Role> res = null;
            try
            {
                res = _appDbContext.Roles.ToList();
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return res;
        }
    }
}
