using iLearn.Data;
using iLearn.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            var res = false;
            try
            {
                await appDbContext.Users.AddAsync(user);
                appDbContext.SaveChanges();
                res = true;
            }
            catch
            {

            }
            return res;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            User res = null;
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    res = appDbContext.Users
                        .Include(u => u.UserRoles)
                        .FirstOrDefault(u => u.Id.ToString() == userId);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return res;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            User res = null;
            try
            {
                res = await appDbContext.Users.Include(u => u.UserRoles)?.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return res;
        }

        //public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        //{
        //    User res = null;
        //    try
        //    {
        //        res = await appDbContext.Users.Include(u => u.UserRoles)?
        //            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    return res;
        //}
    }
}
