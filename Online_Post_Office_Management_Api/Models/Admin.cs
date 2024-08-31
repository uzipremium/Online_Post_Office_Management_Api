using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AccountId { get; set; }

        public Account Account { get; set; }
    }
}
