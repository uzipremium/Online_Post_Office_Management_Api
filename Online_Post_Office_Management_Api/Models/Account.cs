using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace Online_Post_Office_Management_Api.Models
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string RoleId { get; set; }

    }
}