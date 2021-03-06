using WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using WebMVC.Config;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using WebMVC.Helper;

namespace WebMVC.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _apiClient;
        private readonly IHttpContextAccessor _accessor;
        private readonly IServiceProvider _serviceProvider;
        public ReservationService(HttpClient httpClient, IHttpContextAccessor accessor, IServiceProvider serviceProvider)
        {
            _apiClient = httpClient;
            _accessor = accessor;
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.ReservationOperations.Create();
            var content = new StringContent(JsonSerializer.Serialize(reservation), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);

            return response.StatusCode != HttpStatusCode.BadRequest;
        }

        public async Task<bool> DeleteReservationAsync(string id)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.ReservationOperations.Delete(id);
            var response = await _apiClient.DeleteAsync(url);

            return response.StatusCode != HttpStatusCode.BadRequest;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.ReservationOperations.Get();
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Reservation>>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Reservation>> GetOwnersReservations(string id)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var userId = id;
            var url = UrlsConfig.ReservationOperations.GetOwnersRes(userId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Reservation>>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Reservation> GetReservationAsync(string id)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.ReservationOperations.GetById(id);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Reservation>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Reservation>> GetUsersReservations()
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var userId = _accessor.HttpContext.Session.GetString("ID");
            var url = UrlsConfig.ReservationOperations.GetUsersRes(userId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Reservation>>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Reservation>> GetUsersReservations(string id)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var userId = id;
            var url = UrlsConfig.ReservationOperations.GetUsersRes(userId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Reservation>>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Reservation>> GetRoomsReservations(string hotelId, string roomId)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            var url = UrlsConfig.ReservationOperations.GetRoomsRes(hotelId, roomId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Reservation>>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
