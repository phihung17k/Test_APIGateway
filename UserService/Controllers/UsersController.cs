using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services.Interface;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _UserService;
        public UsersController(IUserServices UserService)
        {
            _UserService = UserService;
        }

        [HttpGet]
        public IEnumerable<User> UserList()
        {
            var UserList = _UserService.GetUserList();
            return UserList;

        }

        [HttpGet("{id}")]
        public async Task<User?> GetUserById(int id)
        {
            return await _UserService.GetUserById(id);
        }

        [HttpPost]
        public async Task<User> AddUser(User User)
        {
            return await _UserService.AddUser(User);
        }

        [HttpPut]
        public async Task<User> UpdateUser(User User)
        {
            return await _UserService.UpdateUser(User);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteUser(int id)
        {
            return await _UserService.DeleteUser(id);
        }
    }
}
