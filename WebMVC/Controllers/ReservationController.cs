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
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IReservationService _reservationService;
        private readonly IHttpContextAccessor _accessor;
        public ReservationController(IReservationService reservationService, IHttpContextAccessor accessor)
        {
            _reservationService = reservationService;
            _accessor = accessor;
        }

        // POST: ReservationController/Create
        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            _reservationService.CreateReservationAsync(reservation);
            return Ok();
        }

        // POST: ReservationController/Delete/5
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
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
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
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
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            var res = await _reservationService.GetAllReservationsAsync();
            ViewBag.empty = "";
            if (res == null)
            {
                ViewBag.empty = "0 reservations";
            }

            return View("ListRes",res);
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetUsersReservations()
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            var res = await _reservationService.GetUsersReservations();
            @ViewBag.empty = "";
            if (res.Count == 0)
            {
                @ViewBag.empty = "You don't have any reservations";
            }

            return View("ListRes", res);
        }

        [HttpGet("Reservation/{id:length(24)}")]
        public async Task<ActionResult<List<Reservation>>> GetUsersReservations(string id)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            var res = await _reservationService.GetUsersReservations(id);
            @ViewBag.empty = "";
            if (res.Count == 0)
            {
                @ViewBag.empty = "User don't have any reservations";
            }

            return View("ListRes", res);
        }
    }
}
