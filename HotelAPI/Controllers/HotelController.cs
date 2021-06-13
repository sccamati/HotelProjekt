﻿using HotelAPI.Models;
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
        public ActionResult<Hotel> UpdateHotel(Hotel hotel)
        {
            var rooms = _service.GetHotel(hotel.Id).Rooms.ToList();
            hotel.Rooms = rooms;
            if (_service.UpdateHotel(hotel).ModifiedCount == 0)
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

        [HttpGet("/ownerHotel/{ownerId:length(24)}")]
        public ActionResult<List<Hotel>> GetOwnerHotels(string ownerId)
        {
            return _service.GetOwnerHotel(ownerId);
        }

        //Rooms CRUD        

        [HttpPost("room/")]
        public ActionResult<RoomHotelViewModel> CreateRoom(RoomHotelViewModel roomHotelViewModel)
        {
            var r = _service.CreateRoom(roomHotelViewModel);
            return Ok(roomHotelViewModel);
        }

        [HttpDelete("room/{hotelId:length(24)}/{roomId}")]
        public ActionResult<Hotel> DeleteRoom(string hotelId, string roomId)
        {
            var h = _service.DeleteRoom(hotelId, roomId);
            if (h == null)
            {
                NotFound();
            }
            return Ok();
        }

        [HttpGet("room/{hotelId:length(24)}/{roomId}")]
        public ActionResult GetRoom(string hotelId, string roomId)
        {
            var room = _service.GetRoom(hotelId, roomId);
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

        [HttpGet("rooms/filtred/")]
        public ActionResult<List<RoomHotelViewModel>> GetFiltredRooms(
            [FromQuery] string city,
            [FromQuery] string phrase,
            [FromQuery] int bedForOne,
            [FromQuery] int bedForTwo,
            [FromQuery] int numberOfGuests,
            [FromQuery] decimal price,
            [FromQuery] string standard)
        {
            return _service.GetFiltredRooms(city, phrase, bedForOne, bedForTwo, numberOfGuests, price, standard);
        }
    }
}
