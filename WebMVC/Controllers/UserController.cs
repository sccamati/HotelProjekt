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
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;

        public UserController(IUserService userService, IHttpContextAccessor accessor)
        {
            _userService = userService;
            _accessor = accessor;
        }

        // POST: ReservationController/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            _userService.CreateUserAsync(user);
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
                return BadRequest("Need valid user id");
            }

            var res = await _userService.DeleteUserAsync(id);

            if (res == null)
            {
                return BadRequest($"No user found for id {id}");
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Need valid user id");
            }

            var res = await _userService.GetUserAsync(id);

            if (res == null)
            {
                return BadRequest($"No user found for id {id}");
            }

            return View("UserDetails", res);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            var res = await _userService.GetUsersAsync();

            if (res == null)
            {
                return BadRequest($"0 reservations");
            }

            return View("UserList", res);
        }
    }
}
