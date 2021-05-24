using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebMVC.Config;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient apiClient;

        public UserService(HttpClient httpClient)
        {
            apiClient = httpClient;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var url = UrlsConfig.UserOperations.Create();
            var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            var response = await apiClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var userResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(userResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<User> DeleteUserAsync(string id)
        {
            var url = UrlsConfig.UserOperations.Delete(id);
            var response = await apiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var userResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(userResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var url = UrlsConfig.UserOperations.Get();
            var response = await apiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<User> GetUserAsync(string id)
        {
            var url = UrlsConfig.UserOperations.GetById(id);
            var response = await apiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<User> UpdateUserAsync(string id, User user)
        {
            var url = UrlsConfig.UserOperations.Update();
            var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            var response = await apiClient.PostAsync(url + id, content);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
