using Hotel_Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Gateway.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string id);
        Task<List<User>> GetUsersAsync();
        Task<User> DeleteUserAsync(string id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(string id, User user);
    }
}
