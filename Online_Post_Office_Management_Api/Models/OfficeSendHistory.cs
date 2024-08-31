using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class OfficeSendHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ReceiveId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string OfficeId { get; set; }

        public ReceiveHistory ReceiveHistory { get; set; }

        public Office Office { get; set; }
    }
}
