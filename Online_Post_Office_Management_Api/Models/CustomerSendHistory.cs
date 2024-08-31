using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class CustomerSendHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ReceiveId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }

        public ReceiveHistory ReceiveHistory { get; set; }

        public Customer Customer { get; set; }
    }
}
