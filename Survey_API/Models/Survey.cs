using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Survey_API.Models
{
    public class Survey
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Answers")]
        public Answers Answers { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

    }

    public class Answers
    {
        public string isGymMembership { get; set; }
        public string name { get; set; }
        public string[] gymClasses { get; set; }
        public string fitnessLevel { get; set; }

        public string email { get; set; }
    }
}
