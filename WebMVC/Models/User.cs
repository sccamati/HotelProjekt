using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace WebMVC.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        [StringLength(8, ErrorMessage = "Password is too short. Minimum length is 8")]
        public string Password { get; set; }
        [BsonElement("Role")]
        public RoleType Role { get; set; }

    }

    public enum RoleType
    {
        Admin,
        User,
        Owner
    }
}
