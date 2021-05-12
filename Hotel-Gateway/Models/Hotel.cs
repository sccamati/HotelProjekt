using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Gateway.Models
{
    public class Hotel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("City")]
        public string City { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Rate")]
        public decimal Rate { get; set; }
        [BsonElement("Rooms")]
        public List<Room> Rooms { get; set; }
        [BsonElement("OwnerID")]
        public string OwnerID { get; set; }

        internal Task ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}