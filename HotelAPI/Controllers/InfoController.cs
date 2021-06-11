﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/info")]
    public class InfoController : ControllerBase
    {
        private readonly ILogger<InfoController> _logger;
        public InfoController(ILogger<InfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string DataToSend()
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            Info info = new Info();
            var infoAnswer = JsonSerializer.Serialize<Info>(info, options);
            return infoAnswer;
        }
    }

    public class Info
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }


        public Info()
        {
            ServiceName = "HotelAPI";
            ServiceDescription = "This is my Hotel service.";
        }
    }
}
