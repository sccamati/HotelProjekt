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
            if(service.Create(user))
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("There is already account with this email");
            }
            
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

        [Authorize(Roles = "Admin")]
        [HttpGet("{email}")]
        public ActionResult<List<User>> GetFiltered(string email)
        {
            var users = service.GetUsers();
            return users.Where(u => u.Email.ToUpper().Contains(email.ToUpper())).ToList();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            var user = service.Delete(id);
            if (user.DeletedCount != 1)
            {
                NotFound();
            }
            return Ok();
        }
    }
}
