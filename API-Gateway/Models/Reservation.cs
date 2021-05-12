using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAPI.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("HotelId")]
        public string HotelId { get; set; }
        [BsonElement("RoomID")]
        public string RoomId { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("EndDate")]
        public DateTime EndDate { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }

    }
}
