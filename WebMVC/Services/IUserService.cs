using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string id);
        Task<List<User>> GetUsersAsync(string email);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
    }
}
