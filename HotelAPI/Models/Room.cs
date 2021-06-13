using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelAPI.Models
{
    public class Room
    {
        [BsonElement("Id")]
        public string Id { get; set; }
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

    public enum STANDARD
    {
        Standard,
        Exclusive
    }
}
