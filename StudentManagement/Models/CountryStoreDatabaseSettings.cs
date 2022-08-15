namespace CountryManagement.Models
{
    public class CountryStoreDatabaseSettings : ICountryStoreDatabaseSettings
    {
        public string CountryCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
