using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class ReservationListViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string HotelName { get; set; }
        public string  City { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("EndDate")]
        public DateTime EndDate { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("ReservationDate")]
        public DateTime ReservationDate { get; set; }
    }
}
