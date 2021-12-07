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

    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly IdentityService _identityService;

        public IdentityController(IdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost]
        public ActionResult Login(LoginUser loginUser)
        {
            var user = _identityService.Authenticate(loginUser);

            if (user == null)
                return Unauthorized();

            return Ok(user);
        }

        [Authorize]
        [HttpGet("{token}")]
        public ActionResult RefreshToken(string token)
        {
            var user = _identityService.RefreshToken(token);

            if (user == null)
                return Unauthorized();

            return Ok(user);
        }
    }
}
