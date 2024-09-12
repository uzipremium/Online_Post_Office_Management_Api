using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class Package
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string? SenderId { get; set; } 

        public string? OfficeId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ServiceId { get; set; }

        public decimal Weight { get; set; }

        public string DeliveryNumber { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string DescriptionId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PaymentId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string DeliveryId { get; set; }

        public string Receiver { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
