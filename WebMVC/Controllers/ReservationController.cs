using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Helper;
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
        private readonly IHotelService _hotelService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IServiceProvider _serviceProvider;

        public ReservationController(IReservationService reservationService, IHotelService hotelService, IHttpContextAccessor accessor, IServiceProvider serviceProvider)
        {
            _reservationService = reservationService;
            _hotelService = hotelService;
            _accessor = accessor;
            _serviceProvider = serviceProvider;
        }

        // POST: ReservationController/Create
        [HttpGet]
        public async Task<ActionResult> Create(string hotelId, string roomId, DateTime startDate, DateTime endDate)
        {
            var user = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (user == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (user.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
            }

            RoomHotelViewModel room = await _hotelService.GetRoom(hotelId, roomId);

            Reservation reservation = new Reservation
            {
                HotelId = hotelId,
                RoomId = room.Room.Id,
                UserId = _accessor.HttpContext.Session.GetString("ID"),
                StartDate = startDate,
                EndDate = endDate,
                Price = room.Room.Price
            };
            var res = await _reservationService.CreateReservationAsync(reservation);

            return RedirectToAction("GetUsersReservations");
        }

        // POST: ReservationController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var user = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (user == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (user.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
            }

            ViewBag.error = "";
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.error = $"No reservation found for id {id}";
            }

            var res = await _reservationService.DeleteReservationAsync(id);

            if (!res)
            {
                ViewBag.error = $"No reservation found for id {id}";
            }

            return RedirectToAction("GetUsersReservations");
        }

        [HttpGet]
        public async Task<ActionResult<Reservation>> Details(string id)
        {
            var user = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (user == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (user.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
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
            var user = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (user == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (user.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
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
            var user = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (user == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (user.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
            }

            var res = await _reservationService.GetUsersReservations();
            @ViewBag.empty = "";
            if (res.Count == 0)
            {
                @ViewBag.empty = "You don't have any reservations";
            }

            return View("ListRes", res);
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetOwnersReservations(string id)
        {
            var user = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (user == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (user.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
            }

            var res = await _reservationService.GetOwnersReservations(id);
            @ViewBag.empty = "";
            if (res.Count == 0)
            {
                @ViewBag.empty = "You don't have any reservations for this hotel";
            }

            return View("ListRes", res);
        }

        [HttpGet("Reservation/{id:length(24)}")]
        public async Task<ActionResult<List<Reservation>>> GetUsersReservations(string id)
        {
            var user = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (user == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (user.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
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
