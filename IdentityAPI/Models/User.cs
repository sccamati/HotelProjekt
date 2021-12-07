using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace IdentityAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("Role")]
        public Role Role { get; set; }
        [BsonElement("RefreshToken")]
        public string RefreshToken { get; set; }
    }

    public enum Role
    {
        Admin,
        User,
        Owner
    }
}
