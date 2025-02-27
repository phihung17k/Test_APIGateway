using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserService.Data;
using UserService.Models;
using UserService.Services.Interface;

namespace UserService.Services
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext _dbContext;

        public UserServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(User User)
        {
            EntityEntry<User> result = await _dbContext.Users.AddAsync(User);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteUser(int id)
        {
            User? User = await _dbContext.Users.FindAsync(id);
            if (User == null)
                return false;

            _dbContext.Users.Remove(User);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public IEnumerable<User> GetUserList()
        {
            return _dbContext.Users;
        }

        public async Task<User> UpdateUser(User User)
        {
            EntityEntry<User> result = _dbContext.Users.Update(User);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
