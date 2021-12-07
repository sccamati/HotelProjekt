using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class LoggedUser
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime? ExpiresTime { get; set; }
    }
}
