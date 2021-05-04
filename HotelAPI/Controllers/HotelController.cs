using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly HotelService hotelService;

        public HotelController(HotelService _hotelService)
        {
            hotelService = _hotelService;
        }

        [HttpPost("/create")]
        public ActionResult<Hotel> CreateHotel(Hotel hotel)
        {
            hotelService.CreateHotel(hotel);
            return Json(hotel);
        }

        [HttpGet("/all")]
        public ActionResult<List<Hotel>> GetHotels()
        {
            return hotelService.GetHotels();
        }

        [HttpGet("/get/{id:length(24)}")]
        public ActionResult<List<Hotel>> GetHotel(string id)
        {
            var hotel = hotelService.GetHotel(id);
            return Json(hotel);
        }

        [HttpPost("room/create/{hotelId:length(24)}")]
        public ActionResult<Room> CreateRoom(string hotelId, Room room)
        {
            hotelService.CreateRoom(hotelId, room);
            return Json(room);
        }

        [HttpGet("rooms/all/{hotelId:length(24)}")]
        public ActionResult<List<Room>> GetRooms(string hotelId)
        {
            return hotelService.GetRooms(hotelId);
        }

        [HttpGet("room/get/{hotelId:length(24)}")]
        public ActionResult<Room> GetRoom(string hotelId, int number)
        {
            var room = hotelService.GetRoom(hotelId, number);
            return Json(room);
        }

        [HttpGet("rooms/filtred")]
        public ActionResult<List<Room>> GetFiltredRooms(string city, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, int standard)
        {
            return Json(hotelService.GetFiltredRooms(city, bedForOne, bedForTwo, numberOfGuests, price, standard));
        }

        [HttpGet("/ownerHotels/{ownerId:length(24)}")]
        public ActionResult<List<Hotel>> GetOwnerHotels(string ownerId)
        {
            return hotelService.GetOwnerHotels(ownerId);
        }
    }
}
