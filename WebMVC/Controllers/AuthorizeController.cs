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
            var witam = await _authorizeService.LogInAsync(user);
            if(witam == null)
            {
                BadRequest();
            }
            //Save token in session object
            _accessor.HttpContext.Session.SetString("JWToken", witam.Token);
            return RedirectToAction("Index", "Home");
        }
    }
}
