using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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

            if (res == null)
            {
                return BadRequest($"No reservation found for id {id}");
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Reservation>> GetReservation(string id)
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

            return View("ReservationDetails", res);
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAllReservations()
        {
            var res = await _reservationService.GetAllReservationsAsync();

            if (res == null)
            {
                return BadRequest($"0 reservations");
            }

            return View("ListRes",res);
        }
    }
}
