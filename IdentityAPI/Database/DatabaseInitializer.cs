using IdentityAPI.Models;
using IdentityAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Database
{
    public class DatabaseInitializer
    {
        public static void Seed(UserService userService)
        {
            if (!userService.GetUsers().Any())
            {
                var users = new List<User>()
                {
                    new User()
                    {
                        Id = "123456789123456789123452",
                        Email = "admin@wp.pl",
                        Password = "Password1.",
                        Role = Role.Admin,
                        RefreshToken = ""
                    },
                    new User()
                    {
                        Id = "123456789123456789123451",
                        Email = "user@wp.pl",
                        Password = "Password1.",
                        Role = Role.User,
                        RefreshToken = ""
                    },
                    new User()
                    {
                        Id = "123456789123456789123456",
                        Email = "owner@wp.pl",
                        Password = "Password1.",
                        Role = Role.Owner,
                        RefreshToken = ""
                    },
                };

                users.ForEach(user => userService.Create(user));
            }
        }
    }
}
