using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string RoleName { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}