using CountryManagement.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace CountryManagement.Services
{
    public class CountryService : ICountryService
    {
        public readonly IMongoCollection<Country> _countries;
        public readonly IMongoCollection<BsonDocument> _countries1;
        public CountryService(ICountryStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _countries = database.GetCollection<Country>(settings.CountryCollectionName);
            _countries1 = database.GetCollection<BsonDocument>(settings.CountryCollectionName);

        }
        public Country Create(Country country)
        {
            _countries.InsertOne(country);
            return country;
        }

        public List<Country> Get()
        {
            return _countries.Find(course => true).ToList();
        }
        public Country Get(string id)
        {
            return _countries.Find(country => country.Id.Equals(id)).FirstOrDefault();
        }

        public List<Country> Get(string CountryName, string id)
        {
            /*List<Country> countries = new List<Country>();
            var queryExpr = new BsonRegularExpression(new Regex(CountryName, RegexOptions.None));
            var builder = Builders<Country>.Filter;
            var filter1= builder.Regex("countryName",queryExpr);
            var matchedDoc = _countries.Find(filter1).ToList();*/

            var filter = Builders<Country>.Filter.Regex("countryName", CountryName);
            var result = _countries.Find(filter).ToList();

            /*var filter = Builders<Country>.Filter.Regex("countryName", new BsonRegularExpression(CountryName, "i"));
            //var filter = builder.Regex("countryName",CountryName);
            countries = _countries.Find(filter).ToList();
            foreach (var temp in countries){
                Debug.WriteLine("*********************************");
                Debug.WriteLine(temp.CountryName+" - "+temp.CountryPlate);
            }*/
            /*var filter2 = new BsonDocument { { "countryName", new BsonDocument { { "$regexFind", CountryName }, { "$options", "i" } } } };
            var result = _countries.Find(filter2).ToList();
            foreach (var temp in result)
            {
                Debug.WriteLine("//////////////////////////////////////////");
                Debug.WriteLine(temp.CountryName + " - " + temp.CountryPlate);
            }*/

            /*var filter3 = Builders<BsonDocument>.Filter.Eq("countryName", CountryName);
            var query = _countries1.Find(filter3).ToList();
            foreach (var results in query)
            {
                Debug.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%5%%%%%");
                Debug.WriteLine(results);
            }

            //var spec = new Document();
            //spec.Add("Name", new MongoRegex(".*" + CountryName + ".*", "i"));
            //countries = _countries.Find(country => country.CountryName == CountryName).ToList();*/
            return result;
        }
        public void Remove(string id)
        {
            _countries.DeleteOne(country => country.Id.Equals(id));
        }

        public void Update(string id, Country country)
        {
            _countries.ReplaceOne(country => country.Id.Equals(id), country);
        }
    }
}
