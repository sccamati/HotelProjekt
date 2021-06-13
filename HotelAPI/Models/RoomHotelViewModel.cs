using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI.Models
{
    public class RoomHotelViewModel
    {
        [BsonElement("HotelId")]
        public string HotelId { get; set; }
        public Room Room { get; set; }
    }
}