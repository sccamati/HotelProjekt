using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebMVC.Config;
using WebMVC.Models;

namespace WebMVC.Services
{
    
    public class HotelService : IHotelService
    {
        private readonly HttpClient _apiClient;
        private readonly IHttpContextAccessor _accessor;
        public HotelService(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _apiClient = httpClient;
            _accessor = accessor;
        }


        public async Task<bool> CreateHotel(Hotel hotel)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.CreateHotel();
            var content = new StringContent(JsonSerializer.Serialize(hotel), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);

            return response.StatusCode != HttpStatusCode.BadRequest;
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.UpdateHotel();
            var content = new StringContent(JsonSerializer.Serialize(hotel), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PutAsync(url, content);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Hotel>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> DeleteHotel(string id)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.DeleteHotel(id);
            var response = await _apiClient.DeleteAsync(url);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<Hotel> GetHotel(string id)
        {
            var url = UrlsConfig.HotelOperations.GetHotel(id);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Hotel>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var url = UrlsConfig.HotelOperations.GetHotels();
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Hotel>>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Hotel>> GetOwnerHotels(string ownerId)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.GetOwnerHotels(ownerId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Hotel>>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        //Rooms CRUD        
        public async Task<Room> CreateRoom(string hotelId, Room room)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.CreateRoom(hotelId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Room>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

    public async Task<Hotel> DeleteRoom(string hotelId, int number)
    {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.DeleteRoom(hotelId, number);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Hotel>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

public async Task<Room> GetRoom(string hotelId, int number)
        {
            var url = UrlsConfig.HotelOperations.GetRoom(hotelId, number);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Room>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Room>> GetRooms(string hotelId)
        {
            var url = UrlsConfig.HotelOperations.GetRooms(hotelId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Room>>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Room>> GetFiltredRooms(
            string city,
            string phrase,
            int bedForOne,
            int bedForTwo,
            int numberOfGuests,
            decimal price,
            int standard)
        {
            var url = UrlsConfig.HotelOperations.GetFiltredRooms(city, phrase, bedForOne, bedForTwo, numberOfGuests, price, standard);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Room>>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
