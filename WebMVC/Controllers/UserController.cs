using Microsoft.AspNetCore.Authorization;
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
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IServiceProvider _serviceProvider;

        public UserController(IUserService userService, IHttpContextAccessor accessor, IServiceProvider serviceProvider)
        {
            _userService = userService;
            _accessor = accessor;
            _serviceProvider = serviceProvider;
        }

        // POST: ReservationController/Create
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (loggedUser.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
            }

            await _userService.CreateUserAsync(user);
            return Ok();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {

            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (loggedUser.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
            }

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Need valid user id");
            }

            var res = await _userService.DeleteUserAsync(id);

            if (!res)
            {
                return RedirectToAction("Index", "Authorize");
            }
            return RedirectToAction("GetUsers");
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string id)
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
                return BadRequest("Need valid user id");
            }

            var res = await _userService.GetUserAsync(id);

            if (res == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            return View("UserDetails", res);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUserDetails()
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

            var res = await _userService.GetUserAsync(_accessor.HttpContext.Session.GetString("ID"));

            if (res == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            return View("UserDetails", res);
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers(string email)
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

            var res = await _userService.GetUsersAsync(email);

            if (res == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            return View("UserList", res);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
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
                return BadRequest("Need valid user id");
            }

            var res = await _userService.GetUserAsync(id);

            if (res == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            return View("EditUser", res);
        }
        // POST: ReservationController/Create

        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (loggedUser.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
            }

            var res = await _userService.UpdateUserAsync(user);
            if (res == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            return RedirectToAction("GetUsers");
        }

        [HttpGet]
        public async Task<ActionResult> ChangePassword()
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

            if (string.IsNullOrEmpty(_accessor.HttpContext.Session.GetString("ID")))
            {
                return BadRequest("Need valid user id");
            }

            var res = await _userService.GetUserAsync(_accessor.HttpContext.Session.GetString("ID"));

            if (res == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            return View("NewPassword", res);
        }
        // POST: ReservationController/Create

        [HttpPost]
        public async Task<ActionResult> ChangePassword(User user)
        {
            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));

            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Authorize");
            }

            if (loggedUser.ExpiresTime < DateTime.Now)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var authorizeService = scope.ServiceProvider.GetRequiredService<IAuthorizeService>();
                    await authorizeService.RefreshToken();
                }
            }


            var res = await _userService.UpdateUserAsync(user);
            if (res == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            return RedirectToAction("GetUserDetails");
        }

        [HttpGet]
        public ActionResult Register()
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
                return BadRequest("Need valid user id");
            }

            var res = await _userService.GetUserAsync(id);

            if (res == null)
            {
                return RedirectToAction("Index", "Authorize");
            }
            return View("UserDetails", res);
        }
    }
}
