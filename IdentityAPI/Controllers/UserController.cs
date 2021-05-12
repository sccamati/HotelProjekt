using IdentityAPI.Models;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService service;

        public UserController(UserService _service)
        {
            service = _service;
        }
        
        [HttpPost]
        public ActionResult<User> Create (User user)
        {
            service.Create(user);
            return Json(user);
        }

        [HttpPost("/update/{id:length(24)}")]
        public ActionResult<User> Update(string id, User user)
        {
            service.Update(id, user);
            return Json(user);
        }

        /*[HttpPost("/delete/{id:length(24)}")]
        public ActionResult<> DeleteHotel(string id)
        {
            service.Delete(id);
            return RedirectToAction("Index", "Home");
        }*/

        [HttpGet("{id:length(24)}")]
        public ActionResult<List<User>> GetUser(string id)
        {
            var user = service.GetUser(id);
            return Json(user);
        }

        [HttpGet("/all")]
        public ActionResult<List<User>> GetUsers()
        {
            return service.GetUsers();
        }
    }
}
