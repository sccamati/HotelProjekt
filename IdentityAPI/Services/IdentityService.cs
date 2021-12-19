
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
                    new Claim("id", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                
                Expires = DateTime.Now.AddMinutes(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };


            var refreshTokenKey = Encoding.ASCII.GetBytes(_key);
            var refreshTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),

                Expires = DateTime.Now.AddMinutes(10),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

            user.RefreshToken = tokenHandler.WriteToken(refreshToken);

            _users.ReplaceOne(u => u.Id == user.Id, user);

            return new LoggedUser()
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = tokenHandler.WriteToken(refreshToken),
                Id = user.Id, Email = user.Email,
                Role = user.Role.ToString(),
                ExpiresTime = tokenDescriptor.Expires
            };
        }

        public LoggedUser RefreshToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

            

            var Id = tokenHandler.ReadJwtToken(token).Claims.First().Value;
            var user = _users.Find(u => u.Id == Id.ToString()).FirstOrDefault();

            if(token != user.RefreshToken)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),

                Expires = DateTime.Now.AddMinutes(5),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var refreshTokenKey = Encoding.ASCII.GetBytes(_key);
            var refreshTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),

                Expires = DateTime.Now.AddMinutes(10),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(refreshTokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var newRefreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);
            var newToken = tokenHandler.CreateToken(tokenDescriptor);

            user.RefreshToken = tokenHandler.WriteToken(newRefreshToken);
            _users.ReplaceOne(u => u.Id == user.Id, user);

            return new LoggedUser()
            {
                Token = tokenHandler.WriteToken(newToken),
                RefreshToken = tokenHandler.WriteToken(newRefreshToken),
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString(),
                ExpiresTime = tokenDescriptor.Expires
            };
        }
    }
}
