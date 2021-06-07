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
        private readonly HotelService _service;

        public HotelController(HotelService _hotelService)
        {
            _service = _hotelService;
        }

        //Hotels CRUD
        [HttpPost]
        public ActionResult<Hotel> CreateHotel(Hotel hotel)
        {
            var h = _service.CreateHotel(hotel);
            return Ok(hotel);
        }

        [HttpPut]
        public ActionResult<Hotel> UpdateHotel(string id, Hotel hotel)
        {
            if (_service.UpdateHotel(id, hotel).ModifiedCount == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(hotel);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult<List<Hotel>> DeleteHotel(string id)
        {
            var h = _service.DeleteHotel(id);
            if (h.DeletedCount != 1)
            {
                NotFound();
            }
            return Ok();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult GetHotel(string id)
        {
            var hotel = _service.GetHotel(id);
            if (hotel == null)
            {
                NotFound();
            }
            
            return Ok(hotel);
        }

        [HttpGet]
        public ActionResult<List<Hotel>> GetHotels()
        {
            return _service.GetHotels();
        }

        [HttpGet("/ownerHotels/{ownerId:length(24)}")]
        public ActionResult<List<Hotel>> GetOwnerHotels(string ownerId)
        {
            return _service.GetOwnerHotels(ownerId);
        }

        //Rooms CRUD        

        [HttpPost("room/{hotelId:length(24)}")]
        public ActionResult<Room> CreateRoom(string hotelId, Room room)
        {
            var r = _service.CreateRoom(hotelId, room);
            return Ok(room);
        }

        [HttpPost("room/{id:length(24)}")]
        public ActionResult<Hotel> DeleteRoom(string id, int number)
        {
            var h = _service.DeleteRoom(id, number);
            if (h == null)
            {
                NotFound();
            }
            return Ok();
        }

        [HttpGet("room/{hotelId:length(24)}")]
        public ActionResult GetRoom(string hotelId, int number)
        {
            var room = _service.GetRoom(hotelId, number);
            if (room == null)
            {
                NotFound();
            }

            return Ok(room);
        }

        [HttpGet("rooms/all/{hotelId:length(24)}")]
        public ActionResult<List<Room>> GetRooms(string hotelId)
        {
            return _service.GetRooms(hotelId);
        }      
        

        [HttpGet("rooms/filtred")]
        public ActionResult<List<Room>> GetFiltredRooms(string city, string phrase, int bedForOne, int bedForTwo, int numberOfGuests, decimal price, int standard)
        {
            return _service.GetFiltredRooms(city, phrase, bedForOne, bedForTwo, numberOfGuests, price, standard);
        }
    }
}
