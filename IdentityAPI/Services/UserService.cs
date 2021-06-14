using IdentityAPI.Models;
using IdentityAPI.Security;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace IdentityAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("UsersDb"));
            var database = client.GetDatabase("UsersDb");
            _users = database.GetCollection<User>("Users");
        }

        public User Create(User user)
        {
            if(_users.Find(u => u.Email == user.Email).CountDocuments() == 1)
            {
                user.Id = "error";
                return user;
            }
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = Hash.GetHash(sha256Hash, user.Password);
                user.Password = hash;
            }
            _users.InsertOne(user);
            return user;
        }

        public ReplaceOneResult Update(User user)
        {
            var u = GetUser(user.Id);
            if(user.Password != u.Password)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    string hash = Hash.GetHash(sha256Hash, user.Password);
                    user.Password = hash;
                }
            }
            return _users.ReplaceOne(u => u.Id == user.Id, user);
        }

        public DeleteResult Delete(string id)
        {
            return _users.DeleteOne(u => u.Id == id);
        }

        public User GetUser(string id) => _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public List<User> GetUsers() => _users.Find(user => true).ToList();
    }
}
