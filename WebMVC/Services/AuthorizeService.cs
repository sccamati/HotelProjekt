using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebMVC.Config;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly HttpClient apiClient;

        public AuthorizeService(HttpClient httpClient)
        {
            apiClient = httpClient;
        }
        public async Task<LoggedUser> LogInAsync(LoginUser user)
        {
            var url = UrlsConfig.AuthorizeOperations.LogIn();
            var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            var response = await apiClient.PostAsync(url, content);
            var reservationResponse = await response.Content.ReadAsStringAsync();
            var u = JsonSerializer.Deserialize<LoggedUser>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                u.Token = "";
            }
            return u;
        }
    }
}
