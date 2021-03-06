using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;
using ReservationAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ReservationController : Controller
    {
        private readonly ReservationService _service;

        public ReservationController(ReservationService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet]
        public ActionResult<List<Reservation>> Get()
        {
            return _service.GetReservations();
        }
        [Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Reservation> Get(string id)
        {
            var reservation = _service.GetReservation(id);
            if(reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }
        [Authorize]
        [HttpGet("UsersRes/{id:length(24)}")]
        public ActionResult<Reservation> GetUsersReservation(string id)
        {
            var reservation = _service.GetUserReservations(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }
        [Authorize]
        [HttpGet("OwnersRes/{id:length(24)}")]
        public ActionResult<Reservation> GetOwnersReservation(string id)
        {
            var reservation = _service.GetOwnersReservations(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpGet("RoomsRes/{hotelId:length(24)}/{roomId}")]
        public ActionResult<Reservation> GetRoomsReservation(string hotelId, string roomId)
        {
            var reservation = _service.GetRoomsReservations(hotelId, roomId);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }
        [Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            if (_service.Delete(id).DeletedCount == 0)
            {
                return NotFound();
            }
            return Ok();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            _service.Create(reservation);
            return Ok(reservation);
        }
        [Authorize]
        [HttpPut]
        public ActionResult Put(string id, Reservation reservation)
        {
            if(_service.Update(id, reservation).ModifiedCount == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(reservation);
            }
        }
    }
}
