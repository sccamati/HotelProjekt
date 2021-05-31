using IdentityAPI.Models;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult Create(User user)
        {
            var u = service.Create(user);
            if(u.Id == "error")
            {
                return BadRequest("There is already account with this email");
            }
            return Ok(user);
        }
        [Authorize]
        [HttpPut]
        public ActionResult Update(User user)
        {
            if (service.Update(user).ModifiedCount == 0)
            {
                return NotFound();
            }
                
            return Ok(user);
        }
        [Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult Get(string id)
        {
            var user = service.GetUser(id);
            if(user == null)
            {
                NotFound();
            }
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return service.GetUsers();
        }

    }
}
