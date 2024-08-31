using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class ReceiveHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PackageId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string OfficeId { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Package Package { get; set; }

        public Office Office { get; set; }
    }
}
