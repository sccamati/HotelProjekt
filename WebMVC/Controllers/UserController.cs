using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id:length(24)}")]
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

            if (!res)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("GetUsers");
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
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
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
            return View("EditUser", res);
        }
        // POST: ReservationController/Create

        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            if (_accessor.HttpContext.Session.GetString("JWToken") == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            var res = await _userService.UpdateUserAsync(user);
            if (res == null)
            {
                return BadRequest($"Error wbile editing");
            }
            return RedirectToAction("GetUsers");
        }

        [HttpGet]
        public  ActionResult Register()
        {
            ViewBag.register = "";
            return View("RegisterUser");
        }
        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            user.Role = RoleType.User;
            var res = await _userService.CreateUserAsync(user);
            if (!res)
            {
                ViewBag.register = "Register fail.";
            }
            else
            {
                ViewBag.register = "Register success. Now you can login";
            }
            return View("RegisterUser");
        }

        [HttpGet]
        public ActionResult AdminRegister()
        {
            ViewBag.register = "";
            return View("AdminRegister");
        }
        [HttpPost]
        public async Task<ActionResult> AdminRegister(User user)
        {
            var res = await _userService.CreateUserAsync(user);
            if (!res)
            {
                ViewBag.register = "Register fail.";
            }
            else
            {
                ViewBag.register = "Register success.";
            }
            return View("AdminRegister");
        }
        [HttpGet]
        public async Task<ActionResult> Details(string id)
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
    }
}
