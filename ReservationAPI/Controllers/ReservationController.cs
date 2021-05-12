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
        private readonly ReservationService service;

        public ReservationController(ReservationService _service)
        {
            service = _service;
        }

        [HttpGet]
        public ActionResult<List<Reservation>> GetReservations()
        {
            return service.GetReservations();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<List<Reservation>> GetReservation(string id)
        {
            var reservation = service.GetReservation(id);
            return Json(reservation);
        }

        [HttpGet("delete/{id:length(24)}")]
        public ActionResult<List<Reservation>> DeleteReservation(string id)
        {
            return service.DeleteReservation(id);
        }

        [HttpPost]
        public ActionResult<Reservation> Create(Reservation reservation)
        {
            service.Create(reservation);
            return Json(reservation);
        }


    }
}
