using API_Gateway.Config;
using Microsoft.Extensions.Options;
using ReservationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_Gateway.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient apiClient;
        private readonly UrlsConfig urls;

        public ReservationService(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            apiClient = httpClient;
            urls = config.Value;
        }

        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            var url = urls.Reservations + UrlsConfig.ReservationOperations.Create(reservation);
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
            var url = urls.Reservations + UrlsConfig.ReservationOperations.Delete(id);
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
            var url = urls.Reservations + UrlsConfig.ReservationOperations.Get();
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
            var url = urls.Reservations + UrlsConfig.ReservationOperations.GetById(id);
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
