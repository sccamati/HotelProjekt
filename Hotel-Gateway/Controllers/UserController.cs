using Hotel_Gateway.Models;
using Hotel_Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: ReservationController/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            _userService.CreateUserAsync(user);
            return Ok();
        }

        // POST: ReservationController/Delete/5
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Need valid reservation id");
            }

            var res = await _userService.DeleteUserAsync(id);

            if (res == null)
            {
                return BadRequest($"No reservation found for id {id}");
            }

            return Ok();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Need valid reservation id");
            }

            var res = await _userService.GetUserAsync(id);

            if (res == null)
            {
                return BadRequest($"No reservation found for id {id}");
            }

            return res;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var res = await _userService.GetUsersAsync();

            if (res == null)
            {
                return BadRequest($"0 users");
            }

            return res;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<User>> UpdateUser(string id, User user)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Need valid reservation id");
            }

            var res = await _userService.UpdateUserAsync(id, user);
            if (res == null)
            {
                return BadRequest($"No reservation found for id {id}");
            }

            return res;
        }
    }
}
