using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IHttpContextAccessor _accessor;
        private readonly IHotelService _service;

        public HotelController(IHotelService hotelService, IHttpContextAccessor accessor)
        {
            _service = hotelService;
            _accessor = accessor;
        }

        /*public async Task<bool> CreateHotel(Hotel hotel)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.CreateHotel();
            var content = new StringContent(JsonSerializer.Serialize(hotel), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);

            return response.StatusCode != HttpStatusCode.BadRequest;
        }

        public async Task<Hotel> UpdateHotel(string id, Hotel hotel)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.UpdateHotel(id);
            var response = await _apiClient.GetAsync(url);

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

            return response.StatusCode != HttpStatusCode.BadRequest;
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
        */

        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetHotels()
        {
            var hotels = await _service.GetHotels();

            if (hotels == null)
            {
                return BadRequest($"0 reservations");
            }

            return View("ListHotels", hotels);
        }
        /*
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

        public async Task<List<Room>> GetFiltredRooms(string city, string phrase, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, int standard)
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
        */
    }
}
