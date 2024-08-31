using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Online_Post_Office_Management_Api.Models
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal BaseRate { get; set; }

        public decimal RatePerKg { get; set; }

        public decimal RatePerKm { get; set; }
    }
}
