using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Survey_API.Models
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Text")]
        public string Text { get; set; }

        [BsonElement("Options")]
        public string[] Options { get; set; }

        [BsonElement("Format")]
        public Format Format { get; set; }

    }

    public enum Format
    {
        [BsonRepresentation(BsonType.String)]
        SingleSelect,
        [BsonRepresentation(BsonType.String)]
        MutliSelect,
        [BsonRepresentation(BsonType.String)]
        Text,
    }
}
