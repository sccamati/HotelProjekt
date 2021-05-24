
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using IdentityAPI.Models;
using IdentityAPI.Security;

namespace IdentityAPI.Services
{
    public class IdentityService
    {
        private readonly IMongoCollection<User> _users;
        private readonly string _key;
        public IdentityService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("UsersDb"));
            var database = client.GetDatabase("UsersDb");
            _users = database.GetCollection<User>("Users");
            _key = configuration.GetSection("SecretKey").Value;
        }

        
        public LoggedUser Authenticate(LoginUser loginUser)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                loginUser.Password = Hash.GetHash(sha256Hash, loginUser.Password);
            }

            var user = _users.Find(u => u.Email == loginUser.Email && u.Password == loginUser.Password).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoggedUser() { Token = tokenHandler.WriteToken(token), Email = user.Email, Role = user.Role.ToString() };
        }
    }
}
