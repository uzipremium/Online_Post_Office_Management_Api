using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class Delivery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime SendDate { get; set; }

        public string DeliveryStatus { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string StartOfficeId { get; set; }

        public string CurrentLocation { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EndOfficeId { get; set; }

        public DateTime? DeliveryDate { get; set; }

    }
}
