using IdentityAPI.Models;
using IdentityAPI.Security;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IdentityAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> users;

        public UserService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("HotelDb"));
            var database = client.GetDatabase("HotelDb");
            users = database.GetCollection<User>("Users");
        }

        public List<User> GetUsers() => users.Find(user => true).ToList();
        public User GetUser(string id) => users.Find<User>(user => user.Id == id).FirstOrDefault();
        public User Create(User user)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = Hash.GetHash(sha256Hash, user.Password);
                user.Password = hash;
            }

            users.InsertOne(user);
            return user;
        }
    }
}
