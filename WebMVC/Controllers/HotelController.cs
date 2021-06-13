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

        [HttpGet]
        public ActionResult CreateHotel()
        {
            ViewBag.createHotel = "";
            return View("HotelCreate");
        }

        [HttpPost]
        public async Task<ActionResult> CreateHotel(Hotel hotel)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            hotel.OwnerID = _accessor.HttpContext.Session.GetString("ID");

            await _hotelService.CreateHotel(hotel);
            
            return RedirectToAction("GetOwnerHotel", "Hotel", new { ownerId = _accessor.HttpContext.Session.GetString("ID") });
        }
        
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
            return RedirectToAction("GetOwnerHotel", "Hotel", new { ownerId = _accessor.HttpContext.Session.GetString("ID") });
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
  
        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetOwnerHotel(string ownerId)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            var hotels = await _hotelService.GetOwnerHotel(ownerId);
            ViewBag.empty = "";
            if (hotels == null)
            {
                ViewBag.empty = "empty";
            }

            return View("HotelOwnerList", hotels);
        }

        //Rooms CRUD        

        [HttpGet]
        public ActionResult CreateRoom(string hotelId)
        {
            ViewBag.hotelId = hotelId;
            return View("RoomCreate");
        }


        [HttpPost]
        public async Task<ActionResult> CreateRoom(RoomHotelViewModel roomHotelViewModel)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            await _hotelService.CreateRoom(roomHotelViewModel);

            return RedirectToAction("GetHotel", "Hotel", new { id = roomHotelViewModel.HotelId });
        }

        public async Task<ActionResult> DeleteRoom(string hotelId, string roomId)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            var res = await _hotelService.DeleteRoom(hotelId, roomId);
            if (!res)
            {
                return RedirectToAction("GetHotel", "Hotel", new { id = hotelId });
            }
            return RedirectToAction("GetHotel", "Hotel", new { id = hotelId });
        }

        public async Task<ActionResult<RoomHotelViewModel>> GetRoom(string hotelId, string roomId)
        {
            var room = await _hotelService.GetRoom(hotelId, roomId);

            return View("RoomDetails", room);
        }

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
        public async Task<ActionResult<RoomHotelViewModel>> GetRoomDetails(string hotelId, string roomId)
        {
            var res = await _hotelService.GetRoom(hotelId, roomId);

            return View("RoomDetails", res);
        }
    }
}
