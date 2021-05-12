using API_Gateway.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Gateway.Controllers
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

        // GET: ReservationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReservationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: ReservationController/Create
        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            _reservationService.CreateReservationAsync(reservation);
            return Ok();
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
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
