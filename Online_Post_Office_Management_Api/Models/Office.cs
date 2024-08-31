using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Online_Post_Office_Management_Api.Models
{
    public class Office
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string PinCode { get; set; }

        public string OfficeName { get; set; }

        public string Phone { get; set; }
    }
}
