using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CountryManagement.Models
{
    public class Country
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("countryName")]
        public string CountryName { get; set; } = String.Empty;

        [BsonElement("countryCode")]
        public string CountryCode { get; set; } = String.Empty;

        [BsonElement("countryPlate")]
        public string CountryPlate { get; set; } = String.Empty;


    }
}
