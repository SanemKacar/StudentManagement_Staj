namespace CountryManagement.Models
{
    public interface ICountryStoreDatabaseSettings
    {
        string CountryCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
