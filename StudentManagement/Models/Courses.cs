using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoursesManagement.Models
{
    public class Courses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;  

        [BsonElement("courseName")]
        public string Name { get; set; } = String.Empty;


    }
}
