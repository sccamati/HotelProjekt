using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class RoomHotelViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HotelId { get; set; }
        public Room Room { get; set; }
    }
}
