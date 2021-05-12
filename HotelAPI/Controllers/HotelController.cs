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

        //Hotels CRUD

        [HttpPost]
        public ActionResult<Hotel> Create(Hotel hotel)
        {
            hotelService.CreateHotel(hotel);
            return Json(hotel);
        }

        [HttpPost("{id:length(24)}")]
        public ActionResult<Hotel> Update(string id, Hotel hotel)
        {
            hotelService.UpdateHotel(id, hotel);
            return Json(hotel);
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult<List<Hotel>> DeleteHotel(string id)
        {
            return hotelService.DeleteHotel(id);
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Hotel> GetHotel(string id)
        {
            var hotel = hotelService.GetHotel(id);
            return Json(hotel);
        }

        [HttpGet]
        public ActionResult<List<Hotel>> GetHotels()
        {
            return hotelService.GetHotels();
        }

        [HttpGet("/ownerHotels/{ownerId:length(24)}")]
        public ActionResult<List<Hotel>> GetOwnerHotels(string ownerId)
        {
            return hotelService.GetOwnerHotels(ownerId);
        }

        //Rooms CRUD        

        [HttpPost("room/create/{hotelId:length(24)}")]
        public ActionResult<Room> CreateRoom(string hotelId, Room room)
        {
            hotelService.CreateRoom(hotelId, room);
            return Json(room);
        }

        /*[HttpPost("room/update/{hotelId:length(24)}")]
        public ActionResult<Room> UpdateRoom(string hotelId, int number, Room room)
        {
            hotelService.UpdateRoom(hotelId, number, room);
            return Json(room);
        }*/

        [HttpPost("room/delete/{id:length(24)}")]
        public ActionResult<Hotel> DeleteRoom(string id, int number)
        {
            var hotel = hotelService.DeleteRoom(id, number);
            return Json(hotel);
        }

        [HttpGet("room/get/{hotelId:length(24)}")]
        public ActionResult<Room> GetRoom(string hotelId, int number)
        {
            var room = hotelService.GetRoom(hotelId, number);
            return Json(room);
        }

        [HttpGet("rooms/all/{hotelId:length(24)}")]
        public ActionResult<List<Room>> GetRooms(string hotelId)
        {
            return hotelService.GetRooms(hotelId);
        }      
        

        [HttpGet("rooms/filtred")]
        public ActionResult<List<Room>> GetFiltredRooms(string city, string phrase, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, int standard)
        {
            return Json(hotelService.GetFiltredRooms(city, phrase, bedForOne, bedForTwo, numberOfGuests, price, standard));
        }

        
    }
}
