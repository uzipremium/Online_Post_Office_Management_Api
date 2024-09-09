using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Online_Post_Office_Management_Api.Models
{
    public class History
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; }

        public string FieldName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime ChangeDate { get; set; }
        public string ChangedBy { get; set; }
    }
}
