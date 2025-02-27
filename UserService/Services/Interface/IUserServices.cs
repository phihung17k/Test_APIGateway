using UserService.Models;

namespace UserService.Services.Interface
{
    public interface IUserServices
    {
        public IEnumerable<User> GetUserList();
        public Task<User?> GetUserById(int id);
        public Task<User> AddUser(User User);
        public Task<User> UpdateUser(User User);
        public Task<bool> DeleteUser(int id);
    }
}