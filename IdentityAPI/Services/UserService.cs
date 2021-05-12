﻿using IdentityAPI.Models;
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
        private readonly IMongoCollection<User> users;

        public UserService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("UsersDb"));
            var database = client.GetDatabase("UsersDb");
            users = database.GetCollection<User>("Users");
        }

        public User Create(User user)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = Hash.GetHash(sha256Hash, user.Password);
                user.Password = hash;
            }
            user.Role = Role.User;
            users.InsertOne(user);
            return user;
        }

        public User Update(string id, User user)
        {
            users.ReplaceOne(u => u.Id == id, user);
            return GetUser(id);
        }

        public void Delete(string id)
        {
            var filter = Builders<User>.Filter.Eq("Id", id);
            users.DeleteOne(filter);
        }

        public User GetUser(string id) => users.Find<User>(user => user.Id == id).FirstOrDefault();

        public List<User> GetUsers() => users.Find(user => true).ToList();
        
    }
}
