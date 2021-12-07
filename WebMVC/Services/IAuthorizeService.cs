using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public interface IAuthorizeService
    {
        Task<LoggedUser> LogInAsync(LoginUser loginUser);
        Task<bool> RefreshToken();
    }
}
