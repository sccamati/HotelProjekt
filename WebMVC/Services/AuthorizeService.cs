using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebMVC.Config;
using WebMVC.Helper;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly HttpClient _apiClient;
        private readonly IHttpContextAccessor _accessor;
        public AuthorizeService(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _apiClient = httpClient;
            _accessor = accessor;
        }

        public async Task<LoggedUser> LogInAsync(LoginUser user)
        {
            var url = UrlsConfig.AuthorizeOperations.LogIn();
            var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);
            var authResponse = await response.Content.ReadAsStringAsync();
            var u = JsonSerializer.Deserialize<LoggedUser>(authResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            
            RefreshTokenStorage.RefreshToken = u.RefreshToken;

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                u.Token = "";
            }
            return u;
        }

        public async Task<bool> RefreshToken()
        {

            var token = RefreshTokenStorage.RefreshToken;
            var url = UrlsConfig.AuthorizeOperations.RefreshToken(token);
            var response = await _apiClient.GetAsync(url);
            var authResponse = await response.Content.ReadAsStringAsync();
            var u = JsonSerializer.Deserialize<LoggedUser>(authResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                u.Token = "";
                return false;
            }

            RefreshTokenStorage.RefreshToken = u.RefreshToken;

            _accessor.HttpContext.Session.SetString("JWToken", u.Token);
            _accessor.HttpContext.Session.SetString("ID", u.Id);
            _accessor.HttpContext.Session.SetString("Role", u.Role);
            _accessor.HttpContext.Session.SetString("Email", u.Email);

            return true;
        }
    }
}
