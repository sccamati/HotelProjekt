using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Helper
{
    public static class UserStorage
    {
        public static List<LoggedUser> users = new();
    }
}
