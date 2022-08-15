using CountryManagement.Models;
using MongoDB.Bson;

namespace CountryManagement.Services
{
    public interface ICountryService
    {
        List<Country> Get();
        Country Get(string id);
        List<Country> Get(string CountryName, string id);
        Country Create(Country country);
        void Update(string id, Country country);
        void Remove(string id);
    }
}
