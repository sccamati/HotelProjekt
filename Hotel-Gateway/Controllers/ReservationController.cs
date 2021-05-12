using Hotel_Gateway.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hotel_Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // POST: ReservationController/Create
        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            _reservationService.CreateReservationAsync(reservation);
            return Ok();
        }

        // POST: ReservationController/Delete/5
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Need valid reservation id");
            }

            var res = await _reservationService.DeleteReservationAsync(id);

            if(res == null)
            {
                return BadRequest($"No reservation found for id {id}");
            }

            return Ok();
        }

        [HttpGet("{id:length(24)}")]
        public async  Task<ActionResult<Reservation>> GetReservation(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Need valid reservation id");
            }

            var res = await _reservationService.GetReservationAsync(id);

            if (res == null)
            {
                return BadRequest($"No reservation found for id {id}");
            }

            return res;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAllReservations()
        {
            var res = await _reservationService.GetAllReservationsAsync();

            if (res == null)
            {
                return BadRequest($"0 reservations");
            }

            return res;
        }
    }
}
