using CoursesManagement.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace StudentManagement.Models
{
    public class Student 
    {
        //c# ta fieldlar büyük harfle başlıyor. Mongoda ise küçük harflerle başlaması lazım. Çözmek için 
        //Case insensitive mapping diye bir şey kullanılıyormuş.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        //Stringe çevirdim çünkü ObjectId olduğunda 00000000 gibi gözüküyor id si kursun.

        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("graduated")]
        public bool IsGraduated { get; set; }

        [BsonElement("courses")]
        public List<Courses>? Courses { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; } = String.Empty;

        [BsonElement("age")]
        public int Age { get; set; } 


    }
}
