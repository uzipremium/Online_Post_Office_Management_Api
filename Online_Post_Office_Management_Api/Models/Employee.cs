using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }
    public string Gender { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedDate { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string OfficeId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string AccountId { get; set; }
}