using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using WebMVC.Models;

namespace WebMVC.Services
{
    
    public class HotelService : IHotelService
    {
        private readonly HttpClient _apiClient;
        private readonly IHttpContextAccessor _accessor;
        private readonly IServiceProvider _serviceProvider;
        public HotelService(HttpClient httpClient, IHttpContextAccessor accessor, IServiceProvider serviceProvider)
        {
            _apiClient = httpClient;
            _accessor = accessor;
            _serviceProvider = serviceProvider;

        }

    public async Task<bool> CreateHotel(Hotel hotel)
        {
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

        public async Task<List<Hotel>> GetOwnerHotel(string ownerId)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.GetOwnerHotel(ownerId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var hotelResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Hotel>>(hotelResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        //Rooms CRUD        
        public async Task<bool> CreateRoom(RoomHotelViewModel roomHotelViewModel)
        {
            var url = UrlsConfig.HotelOperations.CreateRoom();
            var content = new StringContent(JsonSerializer.Serialize(roomHotelViewModel), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);

            return response.StatusCode != HttpStatusCode.BadRequest;
        }

        public async Task<bool> DeleteRoom(string hotelId, string roomId)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.DeleteRoom(hotelId, roomId);
            var response = await _apiClient.DeleteAsync(url);

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<RoomHotelViewModel> GetRoom(string hotelId, string roomId)
        {
            var url = UrlsConfig.HotelOperations.GetRoom(hotelId, roomId);
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var roomResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<RoomHotelViewModel>(roomResponse, new JsonSerializerOptions
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

        public async Task<List<RoomHotelViewModel>> GetFiltredRooms(
            string city,
            string phrase,
            int bedForOne,
            int bedForTwo,
            int numberOfGuests,
            decimal price,
            string standard,
            string dateStart,
            string dateEnd
            )
        {
            var url = UrlsConfig.HotelOperations.GetFiltredRooms(city, phrase, bedForOne, bedForTwo, numberOfGuests, price, standard, dateStart, dateEnd);
            
            var response = await _apiClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var roomResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<RoomHotelViewModel>>(roomResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
