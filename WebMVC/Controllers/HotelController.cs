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
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService, IHttpContextAccessor accessor)
        {
            _hotelService = hotelService;
            _accessor = accessor;
        }

       /* public async Task<ActionResult<bool>> CreateHotel(Hotel hotel)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessor.HttpContext.Session.GetString("JWToken"));
            var url = UrlsConfig.HotelOperations.CreateHotel();
            var content = new StringContent(JsonSerializer.Serialize(hotel), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);

            return response.StatusCode != HttpStatusCode.BadRequest;
        }
        */
        [HttpPut]
        public async Task<ActionResult<Hotel>> UpdateHotel(Hotel hotel)
        {
            var hotels = await _hotelService.UpdateHotel(hotel);
            ViewBag.empty = "";
            if (hotels == null)
            {
                ViewBag.empty = "No hotel found";
            }

            return View("HotelEdit", hotels);
        }

        [HttpGet]
        public async Task<ActionResult> EditHotel(string id)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Need valid user id");
            }

            var res = await _hotelService.GetHotel(id);

            if (res == null)
            {
                return BadRequest($"No user found for id {id}");
            }
            return View("HotelEdit", res);
        }

        [HttpPost]
        public async Task<ActionResult> EditHotel(Hotel hotel)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            var res = await _hotelService.UpdateHotel(hotel);
            if (res == null)
            {
                return BadRequest($"Error wbile editing");
            }
            return RedirectToAction("GetHotels");
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult> DeleteHotel(string id)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            var res = await _hotelService.DeleteHotel(id);
            if (!res)
            {
                return RedirectToAction("GetHotels");
            }
            return RedirectToAction("GetHotels");
        }

        [HttpGet]
        public async Task<ActionResult<Hotel>> GetHotel(string id)
        {
            var hotels = await _hotelService.GetHotel(id);
            ViewBag.empty = "";
            if (hotels == null)
            {
                ViewBag.empty = "No hotel found";
            }

            return View("HotelDetails", hotels);
        }

        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetHotels()
        {
            var hotels = await _hotelService.GetHotels();
            ViewBag.empty = "";
            if (hotels == null)
            {
                ViewBag.empty = "empty";
            }

            return View("HotelList", hotels);
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
        */

        public async Task<ActionResult<List<RoomHotelViewModel>>> GetFiltredRooms(
            string city,
            string phrase,
            int bedForOne,
            int bedForTwo,
            int numberOfGuests,
            decimal price,
            string standard,
            string dateStart,
            string dateEnd)
        {
            List<STANDARD> sTANDARDs = new List<STANDARD>();
            sTANDARDs.Add(STANDARD.Standard);
            sTANDARDs.Add(STANDARD.Exclusive);
            ViewBag.Standard = sTANDARDs;
            var res = await _hotelService.GetFiltredRooms(city, phrase, bedForOne, bedForTwo, numberOfGuests, price, standard, dateStart, dateEnd);
            ViewBag.empty = "";
            if (res == null)
            {
                ViewBag.empty = "empty";
            }

            return View("RoomList", res);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetRoomDetails(string hotelId, string roomId)
        {
            var res = await _hotelService.GetRoom(hotelId, roomId);

            return View("RoomDetails", res);
        }
    }
}
