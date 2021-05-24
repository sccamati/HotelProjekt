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
    [Authorize]
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
            service.Create(user);
            return Ok(user);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id, User user)
        {
            if (service.Update(id, user).ModifiedCount == 0)
            {
                return NotFound();
            }
                
            return Ok();
        }

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

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return service.GetUsers();
        }

    }
}
