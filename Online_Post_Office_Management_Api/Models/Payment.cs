using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Status { get; set; }

        public DateTime TransactionTime { get; set; }

        public decimal Cost { get; set; }
    }
}
