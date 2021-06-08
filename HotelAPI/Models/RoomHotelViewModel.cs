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
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HotelId { get; set; }
        [BsonElement("Number")]
        public int Number { get; set; }
        [BsonElement("BedForOne")]
        public int BedForOne { get; set; }
        [BsonElement("BedForTwo")]
        public int BedForTwo { get; set; }
        [BsonElement("NumberOfGuests")]
        public int NumberOfGuests { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }
        [BsonElement("Standard")]
        public STANDARD Standard { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
    }
}