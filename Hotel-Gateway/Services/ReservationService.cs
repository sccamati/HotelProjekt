using Hotel_Gateway.Config;
using Microsoft.Extensions.Options;
using Hotel_Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotel_Gateway.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient apiClient;

        public ReservationService(HttpClient httpClient)
        {
            apiClient = httpClient;
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            var url = UrlsConfig.ReservationOperations.Create();
            var content = new StringContent(JsonSerializer.Serialize(reservation), System.Text.Encoding.UTF8, "application/json");
            var response = await apiClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Reservation>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Reservation> DeleteReservationAsync(string id)
        {
            var url = UrlsConfig.ReservationOperations.Delete(id);
            var response = await apiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Reservation>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            var url = UrlsConfig.ReservationOperations.Get();
            var response = await apiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Reservation>>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Reservation> GetReservationAsync(string id)
        {
            var url = UrlsConfig.ReservationOperations.GetById(id);
            var response = await apiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var reservationResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Reservation>(reservationResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

    }
}
