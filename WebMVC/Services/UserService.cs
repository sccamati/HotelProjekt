using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebMVC.Config;
using WebMVC.Helper;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _apiClient;
        private readonly IHttpContextAccessor _accessor;
        private readonly IServiceProvider _serviceProvider;
        public UserService(HttpClient httpClient, IHttpContextAccessor accessor, IServiceProvider serviceProvider)
        {
            _apiClient = httpClient;
            _accessor = accessor;
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var url = UrlsConfig.UserOperations.Create();
            var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);

            return response.StatusCode != HttpStatusCode.BadRequest;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.UserOperations.Delete(id);
            var response = await _apiClient.DeleteAsync(url);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<User> GetUserAsync(string id)
        {

            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.UserOperations.GetById(id);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.UserOperations.Update();
            var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PutAsync(url, content);

            response.EnsureSuccessStatusCode();

            var userResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(userResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<User>> GetUsersAsync(string email)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.UserOperations.Get(email);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
