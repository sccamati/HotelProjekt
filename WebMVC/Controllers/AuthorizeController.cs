using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Helper;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class AuthorizeController : Controller
    {

        private readonly IAuthorizeService _authorizeService;
        private readonly IHttpContextAccessor _accessor;
        public AuthorizeController(IAuthorizeService authorizeService, IHttpContextAccessor accessor)
        {
            _authorizeService = authorizeService;
            _accessor = accessor;
        }
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginUser user)
        {
            var u = await _authorizeService.LogInAsync(user);
            ViewBag.error = "";
            if (u.Token == null)
            {
                ViewBag.error = "failed to login try again";
                return View();
            }

            var loggedUser = UserStorage.users.SingleOrDefault(u => u.Email == user.Email);
            if(loggedUser == null)
            {
                UserStorage.users.Add(u);
            }
            else
            {
                loggedUser.RefreshToken = u.RefreshToken;
                loggedUser.Token = u.Token;
            }
            

            //Save token in session object
            _accessor.HttpContext.Session.SetString("ID", u.Id);
            _accessor.HttpContext.Session.SetString("Role", u.Role);
            _accessor.HttpContext.Session.SetString("Email", u.Email);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var user = UserStorage.users.Single(u => u.Id == _accessor.HttpContext.Session.GetString("ID"));
            UserStorage.users.Remove(user);

            _accessor.HttpContext.Session.Remove("ID");
            _accessor.HttpContext.Session.Remove("Role");
            _accessor.HttpContext.Session.Remove("Email");
            return RedirectToAction("Index", "Home");
        }
    }
}
